# encoding: UTF-8

require_relative 'command_line_parser'
require_relative 'mars_plateau'
require_relative 'nasa_rover'

module ThoughtWorksMarsRoversProblem

	class MarsRoversApp
		def initialize remote_input_stream = $<, remote_output_stream = $>, remote_error_stream = $stderr
			@in = remote_input_stream
			@out =  remote_output_stream
			@err = remote_error_stream
		end

		def run_remotely_controlled_mars_terrain_exploration
			
			begin
				@out.puts "Waiting for input:"
				
				begin					
					parser = CommandLineParser.new @in
					parser.read_and_parse()
					
				rescue => e
					show_parse_error_message e					
					return
				end
				
				@out.puts "\nOutput:"
				
				plateau = MarsPlateau.new parser.plateau_upper_right_coordinates
				
				occupied_spots = []				
				2.times do |r|
					rover = NasaRover.new parser.rovers[r].position, parser.rovers[r].heading, plateau, occupied_spots.clone
										
					parser.rovers[r].instructions.each { |ins| rover.execute! ins }
					
					occupied_spots.push rover.position

					@out.puts "#{rover.position[0]} #{rover.position[1]} #{PointOfCompass.to_s(rover.heading)}\n"
					@out.flush														
				end
				
			rescue MarsPlateau::InvalidCoordinatesError => e
				show_plateau_error_message e				
			rescue NasaRover::OverstepPlateauError, NasaRover::TakenSpotError => e
				show_rover_error_message e
			rescue => e
				show_unhandled_error_message e
			end

		end

	private
		def show_parse_error_message e
			@err.puts "\nError:"
			@err.puts "Execution aborted for generic input error, check the input."
			@err.puts "Diagnostic message: #{e.message}"
			print_usage
			@err.flush
		end
		
		def show_plateau_error_message e
			@err.puts "\nError:"
			@err.puts "Execution aborted for Plateau error #{e.message}"
			print_usage
		end

		def show_rover_error_message e
			@err.puts "\nError:"
			@err.puts "Execution aborted for positioning error: #{e.message}"			
			@err.flush
		end 

		def show_unhandled_error_message e
			@err.puts "\nError:"
			@err.puts "Rosie the Robot Maid malfunctions."
			@err.puts "Call The Spacely's Space Sprockets and ask George Jetson to fix her!"
			@err.puts "Here follow the diagnostic message for George Jetson:"
			@err.puts "#{e.class.to_s}"
			@err.puts "#{e.message}"
			@err.puts e.backtrace.join("\n")
			@err.puts "EOF"
			@err.flush
		end
			
		def print_usage 
			@err.puts  "\nUsage:"
			@err.puts  "<x> <y>"
			@err.puts  "<x> <y> <poin of compass>"
			@err.puts  "commands"
			@err.puts  "<x> <y> <poin of compass>"
			@err.puts  "commands"
			@err.puts  "Where:"
			@err.puts  "- poin of compass: can be uppercase N, S,W or E. "
			@err.puts  "- commands: a sequence without space of uppercase L, R or M"
		end

	end
end

if __FILE__ == $PROGRAM_NAME	
	app = ThoughtWorksMarsRoversProblem::MarsRoversApp.new 
	app.run_remotely_controlled_mars_terrain_exploration()
end
