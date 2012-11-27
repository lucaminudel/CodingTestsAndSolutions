require_relative 'point_of_compass'
require 'matrix'

module ThoughtWorksMarsRoversProblem
	
	class NasaRover
		attr_reader :position, :heading
		
		class OverstepPlateauError < ArgumentError;  end		
		class TakenSpotError < ArgumentError; end
		
		def initialize position, heading, plateau, obstacles = []

			if !plateau.include_coordinates?(position)
				raise OverstepPlateauError.new "Rover cannot be deployed outside the plateau at (#{position[0]}, #{position[1]}) to prevent damage."
			end

			if obstacles.include?(position)
				raise TakenSpotError.new "Rover cannot be deployed at (#{position[0]}, #{position[1]}) because there is another rover there."
			end									

			@position = position
			@heading = heading
			@plateau = plateau
			@obstacles = obstacles
						
			@left_rotation_matrix = Matrix[[0, -1], [1, 0]]
			@right_rotation_matrix = @left_rotation_matrix * -1
		end
		
		def execute! single_instruction
			send "execute_#{single_instruction}"
		end
		
	private
		def execute_L 
			@heading = @left_rotation_matrix * @heading
		end
		
		def execute_R
			@heading = @right_rotation_matrix * @heading
		end
		
		def execute_M
			position = @position + @heading
			
			if !@plateau.include_coordinates?(position)
				raise OverstepPlateauError.new "Rover cannot move forward outside the plateau at (#{position[0]}, #{position[1]}) to prevent damage."
			end				
			
			if @obstacles.include?(position)
				raise TakenSpotError.new "Rover cannot move forward at (#{position[0]}, #{position[1]}) because there is another rover there."
			end									
		
			@position = position
		end
	end	
end