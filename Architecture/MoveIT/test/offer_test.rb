require 'test/unit'

require_relative '../offer'

class TestOffer < Test::Unit::TestCase
	def setup
		@offer_builder = OfferBuilder.new
	end
	
	def test_current_pricing_version
		offer = @offer_builder.build
		
		assert_equal(1, offer.pricing_version)
	end
	
	def test_offer_date_is_the_one_when_the_offer_is_calculated
		expected_offer_date = Time.new(2012, 10, 12, 10, 34, 56.546)
		time_source_stub = TimeStub.new.stub_now(expected_offer_date)
		
		offer = @offer_builder.with_time_source_equals_to(time_source_stub).build
		
		assert_equal(expected_offer_date, offer.date)
	end
	
	def test_empty_offer_should_return_the_minimum_price
		offer = @offer_builder.build
		
		assert_equal(1000, offer.price)
	end

	def test_price_25_percent_vat
		offer = @offer_builder.build
		
		assert_equal(1250, offer.price_including_vat)
	end

	def test_with_piano_offer_should_cost_5000_more
		an_offer_with_piano = @offer_builder.with_piano.build
		an_offer_without_piano = @offer_builder.without_piano.build
		
		assert_equal(5000, an_offer_with_piano.price - an_offer_without_piano.price)
	end

	def test_expected_offer_price_for_given_distance
		assert_offer_price_for_given_distance :given_distance =>  49, :expected_price => 1490
		assert_offer_price_for_given_distance :given_distance =>  50, :expected_price => 5400
		assert_offer_price_for_given_distance :given_distance =>  99, :expected_price => 5792
		assert_offer_price_for_given_distance :given_distance => 100, :expected_price => 10700
		assert_offer_price_for_given_distance :given_distance => 101, :expected_price => 10707
	end
	
	def test_expected_offer_price_for_given_house_area
		assert_offer_price_for_given_house_area :given_house_area =>  49, :expected_price => 1000
		assert_offer_price_for_given_house_area :given_house_area =>  50, :expected_price => 2000
		assert_offer_price_for_given_house_area :given_house_area =>  99, :expected_price => 2000
		assert_offer_price_for_given_house_area :given_house_area => 100, :expected_price => 3000
		assert_offer_price_for_given_house_area :given_house_area => 149, :expected_price => 3000
		assert_offer_price_for_given_house_area :given_house_area => 150, :expected_price => 4000
	end
		
	def test_basament_area_should_count_twice_house_area
		assert_offers_price_are_equal_for :basament => 13, :house => 26
		assert_offers_price_are_equal_for :basament => 37, :house => 74
		assert_offers_price_are_equal_for :basament => 75, :house => 150
	end
	
	def test_offers_in_requirements_examples
		assert_expected_offer :distance => 35,   :house => 30,  :basament => 0,   :piano =>true,  :expected_price => 6350,   :with_vat => 7937.5
		assert_expected_offer :distance => 280, :house => 95,  :basament => 20,  :piano =>false, :expected_price => 35880, :with_vat => 44850
		assert_expected_offer :distance => 100, :house => 200, :basament => 25, :piano =>true,  :expected_price => 69200,  :with_vat => 86500
		assert_expected_offer :distance => 75,   :house => 95,  :basament => 0,   :piano =>false, :expected_price => 11200,  :with_vat => 14000
	end
	
private
	def assert_offer_price_for_given_distance params
		expected_price = params[:expected_price]
		offer = @offer_builder.with_distance_equals_to(params[:given_distance]).build
		
		assert_equal(expected_price, offer.price)		
	end

	def assert_offer_price_for_given_house_area params
		expected_price = params[:expected_price]
		offer = @offer_builder.with_house_area_equals_to(params[:given_house_area]).build
		
		assert_equal(expected_price, offer.price)		
	end

	def assert_offers_price_are_equal_for areas
		offer_house_area = @offer_builder.with_house_area_equals_to(areas[:house]).build
		offer_basament_area = @offer_builder.with_basament_and_garret_area_equals_to(areas[:basament]).build
		
		assert_equal(offer_house_area.price,  offer_basament_area.price)
	end

	def assert_expected_offer params
		offer =@offer_builder
			.with_distance_equals_to(params[:distance])
			.with_house_area_equals_to(params[:house])
			.with_basament_and_garret_area_equals_to(params[:basament])
			.with_piano(params[:piano])
			.build
			
		assert_equal(params[:expected_price], offer.price)
		assert_equal(params[:with_vat], offer.price_including_vat)	
	end
end

class OfferBuilder
	def initialize
		@customer_id = "any customer"
		@from = "everywhere you are"
		@to = "everywhere you wanna go!"
		@distance = 0
		@house_area = 0
		@basament_and_garret_area = 0
		@piano = false
		
		@time_source = Time
	end
	
	def with_piano included = true
		clone = self.clone
		clone.instance_variable_set(:@piano, included)
		return clone
	end

	def without_piano
		clone = self.clone
		clone.instance_variable_set(:@piano, false)
		return clone	
	end
		
	def method_missing method_id, *args
		begin
			instance_variable = method_id.to_s
			instance_variable["with_"] = "@"
			instance_variable["_equals_to"] = ""
		rescue IndexError
			super
		end

		if self.instance_variable_defined?(instance_variable)
			clone = self.clone
			clone.instance_variable_set(instance_variable, *args)
			return clone
		end
		
		super
	end
	
	def build
		return Offer.new(@customer_id, @from, @to, @distance, @house_area, @basament_and_garret_area, @piano, @time_source)
	end
private
	def clone_with_set_variable name, *args
		clone = self.clone
		clone.instance_variable_set(name, *args)
		return clone
	end
end

class TimeStub
	def initialize
		@now = nil
	end
	
	def now
		return @now
	end
	
	def stub_now return_value
		@now = return_value
		return self
	end
end