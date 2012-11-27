require 'matrix'

module ThoughtWorksMarsRoversProblem

	class MarsPlateau
		
		class InvalidCoordinatesError < ArgumentError
		end
		
		attr_reader :upper_right_coordinates, :lower_left_coordinates
		
		def initialize upper_right_coordinates
			upper_right_coordinates.each do |c| 
				raise InvalidCoordinatesError.new "Plateau invalid coordinates, the #{c} in #{upper_right_coordinates} should be non negative."  if c < 0
			end

			@lower_left_coordinates = Vector[0, 0]
			@upper_right_coordinates = upper_right_coordinates
		end

		def include_coordinates? coordinates
			return coordinates[0].between?(min_x, max_x) &&
				  coordinates[1].between?(min_y, max_y)
		end

	private
		def min_x; @lower_left_coordinates[0]; end
		
		def min_y; @lower_left_coordinates[1]; end
		
		def max_x; @upper_right_coordinates[0]; end
		
		def max_y; @upper_right_coordinates[1]; end		
	end
end