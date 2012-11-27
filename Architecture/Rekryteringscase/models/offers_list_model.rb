require_relative '../offer'
require_relative '../customer'

require_relative 'base_model'


class OffersListModel < BaseModel
	attr_reader 

	attr_reader :first_name, :last_name, :email, :offers_list
				
	
	def initialize customer = nil, offers_list = []
		@offers_list = offers_list
		
		super(customer)
	end
	
end

