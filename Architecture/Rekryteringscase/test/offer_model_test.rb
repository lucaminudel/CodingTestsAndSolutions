require 'test/unit'

require_relative '../offer'
require_relative '../customer'
require_relative '../models/offer_model'

class TestOfferModel < Test::Unit::TestCase
	
	def setup
		distance = 100
		house_area = 30
		basement_garret_area = 5
		with_piano = false
		
		@customer = Customer.new "first_name", "last_name", "email@dot.com"

		@offer = Offer.new @customer.id, "from", "to", distance, house_area, basement_garret_area, with_piano
		
		
		@offer_model = OfferModel.new @customer, @offer, "any_url"		
	end
	
	def test_initialization_should_set_all_the_attributes
		nil_attrs = []
		valid_attrs = []
		@offer_model.public_methods(false).each do |sym|
			val = @offer_model.method(sym).call
			nil_attrs.push(sym) if val == nil
			valid_attrs.push(val) if val != nil
		end
		
		assert_equal(0, nil_attrs.size)
		assert(valid_attrs.size > 0)	
	end	
	
	def test_offer_id_should_be_initialized_from_offer
		assert_equal(@offer.id, @offer_model.offer_id)
	end	
end
