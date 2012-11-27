require_relative '../offer'
require_relative '../customer'

require_relative 'base_model'


class OfferModel < BaseModel
	attr_reader 

	attr_reader :offer_user_friendly_id, :offer_id, :first_name, :last_name, :email, 
			:from, :to, :distance, :house_area, :basement_garret_area, :with_piano,
			:price, :price_including_vat,
			:offer_url
				
	
	def initialize customer, offer, offer_url		
		@offer_url = offer_url
		@offer_id = offer.id
		@offer_user_friendly_id = offer.get_user_friendly_id
		
		assign_attributes_from offer
		
		super(customer)
	end
	
end

