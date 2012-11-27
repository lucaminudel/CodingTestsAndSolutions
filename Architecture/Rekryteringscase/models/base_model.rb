class BaseModel

	def initialize customer = nil
		@is_new_customer = !(customer)
		assign_attributes_from (customer)  if customer
	end
		
	def is_new_customer?
		return @is_new_customer
	end

protected
	def assign_attributes_from obj
		(obj.public_methods(false)  & self.public_methods(false)).each do |sym|
			name = "@" + sym.to_s.delete(":")
			instance_variable_set name, obj.method(sym).call
		end
	end	
end
