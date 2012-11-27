require 'test/unit'

require_relative '../customer'
require_relative '../models/offers_list_model'

class TestOffersListModel < Test::Unit::TestCase
	def setup		
		@customer = Customer.new "first_name", "last_name", "email@dot.com"
		
		@offers_list_model = OffersListModel.new @customer
	end
	
	def test_initialization_should_set_all_the_attributes
		nil_attrs = []
		valid_attrs = []
		(@offers_list_model.public_methods(false) &  @customer.public_methods(false)).each do |sym|
			val = @offers_list_model.method(sym).call
			nil_attrs.push(sym) if val == nil
			valid_attrs.push(sym) if val != nil
		end
		
		assert_equal(0, nil_attrs.size)
		assert_equal(@customer.public_methods(false).size - 1, valid_attrs.size)	
	end	
	

end
