require 'test/unit'
require 'stringio'
require_relative '../mars_rovers_app'

module ThoughtWorksMarsRoversProblemAcceptanceTest

	EXTRA_OUTPUT = "Waiting for input:\n\nOutput:\n"

	class TestCommandLineParser < Test::Unit::TestCase
		
		include ThoughtWorksMarsRoversProblem


		def test_golden_path
			input = 
<<EOF
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
EOF
			expected_output = EXTRA_OUTPUT +
						   "1 3 N\n" +
						   "5 1 E\n"
			expected_error = ""
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert_equal(expected_output, output_string)
			assert_equal(expected_error, error_string)
		end

		def test_golden_path_extra_spaces_in_input
			input = 
<<EOF
   5   5  
     1    2    N   
        LMLMLMLMM     
 3  3  E  
  MMRMMRMRRM  
EOF
			expected_output = EXTRA_OUTPUT +
						   "1 3 N\n" +
						   "5 1 E\n"
			expected_error = ""
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert_equal(expected_output, output_string)
			assert_equal(expected_error, error_string)
		end
		

		def test_golden_path_corner_case
			input = 
<<EOF
0 1
0 0 N

0 1 E

EOF
			expected_output = EXTRA_OUTPUT +
						   "0 0 N\n" +
						   "0 1 E\n"
			expected_error = ""
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert_equal(expected_output, output_string)
			assert_equal(expected_error, error_string)
		end
		
		def test_palteau_coordinates_error
			input = 
<<EOF			
5 -5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
EOF
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "Plateau invalid coordinates"
		end
		
		def test_rover_deployed_outside_the_plateau_error
			input = 
<<EOF			
5 5
1 2 N
LMLMLMLMM
3 -1 N
MMRRLLMMRMRRM
EOF
			expected_output = EXTRA_OUTPUT + "1 3 N\n"
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "cannot be deployed outside the plateau"
			assert_equal(expected_output, output_string)
		end

		def test_rover_overstep_plateau_error
			input = 
<<EOF			
5 5
1 2 N
LMLMLMLMM
3 3 N
MMRRLLMMRMRRM
EOF
			expected_output = EXTRA_OUTPUT + "1 3 N\n"
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "cannot move forward outside the plateau"
			assert_equal(expected_output, output_string)
		end

		def test_rover_deploy_collision_error
			input =
<<EOF
5 5
1 2 N
LMLMLMLMM
1 3 W
MM
EOF
			expected_output = EXTRA_OUTPUT + "1 3 N\n"
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			
			assert error_string.include? "cannot be deployed"
			assert error_string.include? "there is another rover there"
			assert_equal(expected_output, output_string)
		end

		def test_rover_moving_forward_collision_error
			input = 
<<EOF			
5 5
1 2 N
LMLMLMLMM
3 3 W
MM
EOF
			expected_output = EXTRA_OUTPUT + "1 3 N\n"
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "cannot move forward"
			assert error_string.include? "there is another rover there"
			assert_equal(expected_output, output_string)
		end

		def test_invalid_input_plateau_corner_error
			input = 
<<EOF			
5 6 7
EOF
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "Wrong number of coordinates"
		end

		def test_invalid_input_rover_deploy_position_error
			input = 
<<EOF			
5 5
1 N
EOF
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "Wrong number of coordinates"
		end

		def test_invalid_input_heading_character_error
			input = 
<<EOF			
5 5
1 2 n
EOF
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "Unknown heading"
		end
		
		def test_invilid_input_rover_instruction_error
			input = 
<<EOF			
5 5
1 2 N
LMLmMLMLMM
EOF
			
			output_string, error_string = simulate_remotely_controlled_mars_terrain_exploration(input)
			
			assert error_string.include? "Unknown instruction"
		end
		
	private
		
		def simulate_remotely_controlled_mars_terrain_exploration input
			begin
				input_stream = StringIO.new input, "r"

				output = ""
				output_stream = StringIO.new output, "w"
				
				error = ""
				error_stream = StringIO.new error, "w"
				
				app = MarsRoversApp.new input_stream, output_stream, error_stream
				app.run_remotely_controlled_mars_terrain_exploration()
				
				return output_stream.string, error_stream.string
				
			ensure
				input_stream.close
				output_stream.close
				error_stream.close
			end
		end
	end
	
end