# encoding: UTF-8

require_relative 'base_model'

class OfferRequestModel < BaseModel
	attr_reader :mandatory_error_message, :content_error_message,
			:from, :to, :distance, :house_area, :basement_garret_area, 
			:piano, :first_name, :last_name, :email
	
	def initialize customer = nil
		@is_input_valid = true

		super(customer)
	end
	
	def self.from_params params
		new_obj = OfferRequestModel.new
		
		new_obj.instance_variable_set(:@piano, "false")
		new_obj.public_methods(false)
			.map{ |sym| sym.to_s }
			.select{ |attr_rd| params.has_key?(attr_rd)}
			.each do |attr_rd| 				
				new_obj.instance_variable_set(("@" + attr_rd).to_s, params[attr_rd].strip())
			end
		
		new_obj.method("validate!").call
		return new_obj
	end

	def is_input_valid?
		return @is_input_valid
	end
		
private		
	def validate!
		addr_max_len = 100
		pos_max_len = 6
		name_max_len = 20
		mandatory_fields = [
			[:from, addr_max_len, "Flytt från"], [:to, addr_max_len, "Flytt till"], [:distance, 6, "Avstånd"], 
			[:basement_garret_area, pos_max_len,  "Vind/Källare"], [:house_area, pos_max_len,  "Bostadens yta"], 
			[:first_name, name_max_len, "Förnamn"], [:last_name, name_max_len, "Efternamn"], [:email, 40, "E-postadress"]
		]		
		
		missing_fields = mandatory_fields
			.select { |field| self.method(field[0]).call().size == 0 }
			.map{ |field| field[2] }		
		content_errors = mandatory_fields
			.select { |field| method(field[0]).call().size > field[1] }
			.map{ |field| "#{field[2]} is too long, should be less then #{field[1]} characters." }
			
		
		pos_regex = /^\d*$/
		name_regex = /^[A-Za-zÀÈÌÒÙàèìòùÁÉÍÓÚáéíóúÃÑÕãñõÄËÏÖÜŸäëïöüŸçÇŒœßØøÅå ']*$/
		simple_email_regex = /\S+@\S+\.\S+/
		fields_format = [	
			[:distance, pos_regex, "Avstånd must be a positive number."], 
			[:basement_garret_area, pos_regex, "Vind/Källare  must be a positive number."], 
			[:house_area, pos_regex, "Bostadens yta  must be a positive number."], 
			[:first_name,  name_regex, "Förnamn should containt only letters."], 
			[:last_name, name_regex, "Efternamn should containt only letters."], 
			[:email, simple_email_regex, "E-postadress should be a valid email address."]
		]					
		content_errors.push( fields_format
			.select { |field| ! (field[1]  =~ method(field[0]).call()) }
			.map { |field| field[2] } )
			
		if missing_fields.size == 1 
			@mandatory_error_message = "Fyll i alla obligatoriska fält: " + missing_fields.join(", ") + "."
		elsif missing_fields.size > 1
			@mandatory_error_message = "Fyll i obligatoriskt fältet: "+ missing_fields.join(", ") + "."
		else 
			@mandatory_error_message =  ""
		end
		
		@content_error_message = content_errors.join("<br>") 
				
		@is_input_valid = (self.mandatory_error_message + self.content_error_message).empty?
	end
end
