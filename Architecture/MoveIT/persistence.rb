require 'yaml'

require_relative 'offer'
require_relative 'customer'

class CustomerAlreadyExistsError < Exception 
end

class OfferNotFoundError < Exception 
end

class CustomerNotFoundError < Exception 
end

class Persistence
	OFFERS_FOLDER = "offers"
	CUSTOMERS_FOLDER = "customers"
	
	def initialize database_path
		@database_path = database_path
	end
	
	def insert_offer_with_new_customer offer, new_customer
		# Operation: 	save the new customer to a Customer_id.txt file and the offer to Offer_id.txt file 
		# Safety:		consistency and atomicity granted with locking.
		# Limitations:	in case of failure of the file-system operation due to HD or OS malfuntioning, manual activity on single files 
		# 			or bug in this software, the atomicity is not granted because there is no transaction log and no crash 
		# 			recovery.
		# Tests to perform for new platforms:
		#			 offer file Offer_id.txt  
		#			    - exsists already
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		#			 original file Customer_id.txt
		#			    - exsists already
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		begin		
			new_customer_filename = File.join(@database_path, CUSTOMERS_FOLDER, "#{ERB::Util.url_encode(new_customer.id)}.txt")
			File::open(new_customer_filename, File::WRONLY|File::EXCL|File::CREAT) do |file|		
				file.flock File::LOCK_EX		

				file.truncate 0
				file.rewind
				YAML::dump(new_customer, file)
				file.fsync
				
				offer_filename = File.join(@database_path, OFFERS_FOLDER, "#{ERB::Util.url_encode(offer.id)}.txt")
				File::open(offer_filename, File::WRONLY|File::EXCL|File::CREAT) do |offer_file| 
					offer_file.flock File::LOCK_EX
					
					offer_file.truncate 0
					offer_file.rewind					
					YAML::dump(offer, offer_file) 
					
					offer_file.flock File::LOCK_UN
				end
								
				file.flock File::LOCK_UN
			end
		rescue  Errno::EEXIST
			raise CustomerAlreadyExistsError.new
		end				
	end
	
	def insert_offer_for_existing_customer offer
		# Operation: 	save the new offer to Offer_id.txt file 
		# Safety:		the save operation grant consistency and atomicity.
		# Limitations:	in case of failure of the file-system operation due to HD or OS malfuntioning, manual activity on single files 
		# 			or bug in this software, the atomicity is not granted because there is no transaction log and no crash 
		# 			recovery.
		# Tests to perform for new platforms:
		#			 offer file Offer_id.txt  
		#			    - exsists already
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		#			 original file Customer_id.txt
		#			    - doesn't exsists 
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		begin		
			existing_customer_filename = File.join(@database_path, CUSTOMERS_FOLDER, "#{ERB::Util.url_encode(offer.customer_id)}.txt")
			File::open(existing_customer_filename, File::RDONLY) do |file|		
				file.flock File::LOCK_EX		
				
				new_offer_filename = File.join(@database_path, OFFERS_FOLDER, "#{ERB::Util.url_encode(offer.id)}.txt")
				File::open(new_offer_filename, File::WRONLY|File::EXCL|File::CREAT) do |offer_file| 
					offer_file.flock File::LOCK_EX
					
					offer_file.truncate 0
					offer_file.rewind					
					YAML::dump(offer, offer_file) 
					
					offer_file.flock File::LOCK_UN
				end
								
				file.flock File::LOCK_UN
			end
		rescue Errno::ENOENT		
			raise CustomerNotFoundError.new 
		end				
	end
	
	def read_offer_with_customer offer_id
		# Operation: 	read a Customer_id.txt file and an offer Offer_id.txt file. 
		# Safety:		files read operation is consistent, and wait until locks are released.
		# Limitations:	
		# Tests to perform in new platforms:
		#			 files Offer_id.txt or Customer_id.txt
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		
		customer_id = offer_id.split("_")[0]
		begin
			lock_conflict_retries_left = LockRetries
			customer = YAML::load_file(File.join(@database_path, CUSTOMERS_FOLDER, ERB::Util.url_encode(customer_id) + ".txt"))
			
		rescue Errno::ENOENT		
			raise CustomerNotFoundError.new 
		rescue Errno::EACCES
			sleep(WaitBeforeRetry)
			lock_conflict_retries_left -= 1
			retry if lock_conflict_retries_left >= 0
								
			raise 
		end
		
		offer = read_offer(offer_id)
		
		return offer, customer		
	end	

	def read_customer_offers_id_list customer
		# Operation: 	read a list of offers as Offer_id.txt files. 
		# Safety:		the list operation is not consistent.
		# Limitations:	files deleted during the read operation could appear in the 
		#			list together with files created during the read operation.			
		offers_id_list = Dir[File.join(@database_path, OFFERS_FOLDER, "#{customer.id}*.txt")]
					.map { |filename| URI::decode(File.basename(filename, ".txt")) }
		
		return offers_id_list
	end


private

	LockRetries = 5
	WaitBeforeRetry = 0.2		
	
	def read_offer offer_id
		# Operation: 	read an offer Offer_id.txt file. 
		# Safety:		files read operation is consistent, and wait until locks are released.
		# Limitations:	
		# Tests to perform in new platforms:
		#			 files Offer_id.txt
		#			    - exsists and is locked 
		#			    - exsists and is created in exclusive mode and not released yet
		begin
			lock_conflict_retries_left = LockRetries
			offer = YAML::load_file(File.join(@database_path, OFFERS_FOLDER, ERB::Util.url_encode(offer_id) + ".txt"))
		rescue Errno::ENOENT		
			raise OfferNotFoundError.new 
		rescue Errno::EACCES
			sleep(WaitBeforeRetry)
			lock_conflict_retries_left -= 1
			retry if lock_conflict_retries_left >= 0
								
			raise 
		end
	end	
end