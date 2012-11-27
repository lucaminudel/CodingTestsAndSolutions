require_relative 'point_of_compass'

module ThoughtWorksMarsRoversProblem

	class CommandLineParser
		
		RoverCommand = Struct.new(:position, :heading, :instructions)
		
		attr_reader :plateau_upper_right_coordinates, :rovers
		
		def initialize remote_input_stream
			@in = remote_input_stream
			@rovers = []
		end
		
		def read_and_parse
			@plateau_upper_right_coordinates = read_plateau_upper_right_coordinates(@in.readline)

			2.times do
				deploy_position, deploy_heading = read_rovers_deploy_position_and_heading(@in.readline)
				control_instructions = read_rover_instructions(@in.readline)
				
				@rovers.push RoverCommand.new deploy_position, deploy_heading, control_instructions.split(//)
			end
		end		
		
	private
		def read_plateau_upper_right_coordinates command_line
			return to_vector(split(command_line))
		end
		
		def read_rovers_deploy_position_and_heading command_line
						
			input_position_and_heading = split(command_line)
									
			input_heading = input_position_and_heading.pop
			if !input_heading.match(/[NSEW]/)
				raise ArgumentError.new("Unknown heading '#{input_heading}', only uppercase N S E and W are valid headings." )
			end
				
			heading = PointOfCompass.const_get("#{input_heading}")
			position = to_vector(input_position_and_heading)
			
			return position, heading
		end
		
		def read_rover_instructions command_line
			command_line = command_line.strip
			unknown_instr = command_line.match(/[^LRM]/)
			if unknown_instr
				raise ArgumentError.new("Unknown instruction '#{unknown_instr}', only uppercase L R and M are valid instructions." )
			end
			
			return command_line
		end
		
		def split command_line
			return command_line.strip.split(/[\s]+/)
		end
		
		def to_vector(command_array)
			coordinates = Vector.elements(command_array.map{ |s| Integer(s, 10) })
			raise ArgumentError.new("Wrong number of coordinates in: '#{command_array.join(' ')}'" ) if coordinates.size != 2
			return coordinates 
		end
	end		
end
