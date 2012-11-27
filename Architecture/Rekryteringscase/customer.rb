require 'securerandom'

class Customer
	attr_reader :id, :first_name, :last_name, :email
	
	def initialize first_name, last_name, email, id = nil
		@id = id || SecureRandom.uuid 
		
		@first_name = first_name
		@last_name = last_name
		@email = email
	end
end