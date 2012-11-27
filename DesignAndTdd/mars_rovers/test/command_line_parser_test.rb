require 'test/unit'
require 'stringio'
require_relative '../command_line_parser'

module ThoughtWorksMarsRoversProblemUnitTest

	class TestCommandLineParser < Test::Unit::TestCase
		
		include ThoughtWorksMarsRoversProblem
		
		def setup
			input = "  123  500\n" +
				   "1   444  S   \n" +
				   "LRLMRLLMLMR  \n" +
				   "  003434   0 W\n" +
				   " MMMRRMMLMLR\n"
			begin
				input_stream = StringIO.new input, "r"
			
				@parser = CommandLineParser.new input_stream
				@parser.read_and_parse
			ensure
				input_stream.close
			end
		end
		
		def test_plateau_coordinates
			assert_equal(Vector[123, 500], @parser.plateau_upper_right_coordinates)
		end
		
		def test_first_rover_deploy_position
			assert_equal(Vector[1, 444], @parser.rovers[0].position)
		end
		
		def test_second_rover_deploy_position
			assert_equal(Vector[3434, 0], @parser.rovers[1].position)
		end

		def test_first_rover_deploy_heading
			assert_equal(PointOfCompass::SOUTH, @parser.rovers[0].heading)
		end

		def test_second_rover_deploy_heading
			assert_equal(PointOfCompass::WEST, @parser.rovers[1].heading)
		end

		def test_first_rover_instructions
			assert_equal("LRLMRLLMLMR", @parser.rovers[0].instructions.join())
		end

		def test_second_rover_instructions
			assert_equal("MMMRRMMLMLR", @parser.rovers[1].instructions.join())
		end
		
	end

	class TestCommandLineParserErrors < Test::Unit::TestCase

		include ThoughtWorksMarsRoversProblem
		
		class DefaultInput
			
			def initialize
				@input =
<<EOF			
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
EOF
			end
						
			def to_s
				return @input
			end
			
			def with_plateau_upper_right_coordinates value
				replace_input 0, value
			end

			def with_first_rover_deploy_position_and_heading value
				replace_input 1, value
			end
			
			def with_first_rover_instructions value
				replace_input 2, value
			end
			
		private
			def replace_input i, value
				input_array = @input.split(/\n/)
				input_array[i] = value
				@input = input_array.join("\n") + "\n"
				
				return self				
			end			
		end
		
		
		def test_default_input_doesnot_raise_error
			assert_nothing_raised { parse DefaultInput.new.to_s }
		end
		
		def test_wrong_number_of_coordinates_raise_error
			assert_raise(ArgumentError) { 
				parse DefaultInput
					.new
					.with_plateau_upper_right_coordinates("5 5 5")
					.to_s 
			}
		end
		
		def test_unknown_heading_should_raise_error
			assert_raise(ArgumentError) { 
				parse DefaultInput
					.new
					.with_first_rover_deploy_position_and_heading("1 1 n")
					.to_s 
			}			
		end
		
		def test_unknown_instruction_raise_error
			assert_raise(ArgumentError) { 
				parse DefaultInput
					.new
					.with_first_rover_instructions("MLRMMx")
					.to_s
			}
		end

	private
		def parse input
			begin
				input_stream = StringIO.new input, "r"
			
				parser = CommandLineParser.new input_stream
				parser.read_and_parse
			ensure
				input_stream.close
			end
		end
	end
end
