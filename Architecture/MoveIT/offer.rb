class Offer
	attr_reader :id, :date, :pricing_version, :customer_id, 
			:from, :to, :distance, :house_area, :basement_garret_area, :with_piano,
			:price, :price_including_vat
			
	
	def initialize customer_id, from, to, distance, house_area, basement_garret_area, with_piano, time_source = Time
		
		@date = time_source.now
		@id = "#{customer_id}_#{@date.strftime("%Y-%m-%d-%H-%M-%S-%L")}"
		@customer_id = customer_id
		@from = from
		@to = to
		@distance = distance
		@house_area = house_area
		@basement_garret_area = basement_garret_area
		@with_piano = with_piano
		
		compute_offer()
	end
	
	def get_user_friendly_id
		return "#{@date.strftime("%Y-%m-%d %H:%M.%S")}"
	end
	
private
	def compute_offer		
		distance_costs = compute_distance_costs(@distance)
		cars_number = compute_cars_number(@house_area, @basement_garret_area)
		piano_costs = compute_piano_costs(@with_piano)
		@pricing_version = 1		
		
		@price = distance_costs * cars_number + piano_costs
		@price_including_vat = @price * 1.25
	end
	
	def compute_distance_costs distance
		fixed_price, price_per_km = case distance
			when 0..49 then  [1000, 10]
			when 50..99 then [5000, 8]
			when 100..Float::INFINITY then [10000, 7]
			else raise ArgumentError.new "Distance #{distance} should a be non negative integer."
		end
			
		return fixed_price + price_per_km * distance
	end
	
	def compute_cars_number house_area, basement_garret_area
		area = house_area + basement_garret_area * 2
		
		return 1 + area.div(50)
	end
	
	def compute_piano_costs with_piano
		return with_piano ? 5000 : 0
	end
	
end
