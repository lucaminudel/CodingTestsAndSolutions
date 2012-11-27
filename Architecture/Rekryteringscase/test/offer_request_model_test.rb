require 'test/unit'

require_relative '../customer'
require_relative '../models/offer_request_model'

class TestOfferRequestModel < Test::Unit::TestCase
	def setup		
	end
	
	def test_initialization_should_set_all_the_attributes
		customer = Customer.new "first_name", "last_name", "email@dot.com"
		
		offer_request_model = OfferRequestModel.new customer
		
		nil_attrs = []
		valid_attrs = []
		(offer_request_model.public_methods(false) &  customer.public_methods(false)).each do |sym|
			val = offer_request_model.method(sym).call
			nil_attrs.push(sym) if val == nil
			valid_attrs.push(sym) if val != nil
		end
		
		assert_equal(0, nil_attrs.size)
		assert_equal(customer.public_methods(false).size - 1, valid_attrs.size)	
	end	
	
	def test_factory_from_params_should_set_all_the_params
		params = {"from" => "from", "to" => "to", "distance" => "123", "house_area" => "100", "basement_garret_area" => "10",
		"first_name" => "firstname", "last_name" => "lastname", "email" => "email@dot.com"}

		offer_request_model  = OfferRequestModel.from_params(params)
		
		nil_results = []
		offer_request_model.public_methods(false)
			.select{ |sym| offer_request_model.method(sym).arity == 0}
			.each do |sym|
				val = offer_request_model.method(sym).call
				nil_results.push(sym) if val  == nil
			end

		copied_values = []
		params.each do |param, param_value|
			val = offer_request_model.method(param).call
			copied_values.push(param) if val == param_value
		end
		
		assert_equal(0, nil_results.size)
		assert_equal(params.size, copied_values.size)
		
	end

end
