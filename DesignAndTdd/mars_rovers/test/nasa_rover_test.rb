require 'test/unit'
require_relative '../nasa_rover'

module ThoughtWorksMarsRoversProblemUnitTest
	class MarsPlateauStub
		attr_reader :upper_right_coordinates, :lower_left_coordinates
		
		def initialize 
			@lower_left_coordinates = Vector[0, 0]
			@upper_right_coordinates = Vector[99, 99]
			
			@stub_include_coordinates = lambda { |coordinates|  return true }
		end

		def include_coordinates? coordinates
			@stub_include_coordinates.call coordinates
		end
		
		def stub_include_coordinates? lamda_block
			@stub_include_coordinates = lamda_block
		end
	end
	
	class TestNasaRover < Test::Unit::TestCase

		include ThoughtWorksMarsRoversProblem
		
		def setup
			@initial_position = Vector[37, 19]
			@initial_heading = PointOfCompass::NORTH
			@rover = NasaRover.new @initial_position,  @initial_heading, MarsPlateauStub.new
		end

		def test_initial_position
			
			assert_equal(@initial_position, @rover.position)
			assert_equal(@initial_heading, @rover.heading)
		end
		
		def test_execute_rotate_left_should_head_west
			@rover.execute! "L"
			
			assert_equal(PointOfCompass::WEST, @rover.heading)
		end
		
		def test_execute_rotate_left_left_should_head_south
			@rover.execute! "L"
			@rover.execute! "L"
			
			assert_equal(PointOfCompass::SOUTH, @rover.heading)
		end
		
		def test_execute_rotate_left_left_left_should_head_east
			@rover.execute! "L"
			@rover.execute! "L"
			@rover.execute! "L"
			
			assert_equal(PointOfCompass::EAST, @rover.heading)
		end
		
		def test_execute_rotate_left_left_left_should_head_back_north
			@rover.execute! "L"
			@rover.execute! "L"
			@rover.execute! "L"
			@rover.execute! "L"
			
			assert_equal(PointOfCompass::NORTH, @rover.heading)
		end

		def test_execute_rotate_right_should_head_east
			@rover.execute! "R"
			
			assert_equal(PointOfCompass::EAST, @rover.heading)
		end

		def test_execute_rotate_right_right_should_head_south
			@rover.execute! "R"
			@rover.execute! "R"
			
			assert_equal(PointOfCompass::SOUTH, @rover.heading)
		end

		def test_execute_rotate_right_right_right_should_head_west
			@rover.execute! "R"
			@rover.execute! "R"
			@rover.execute! "R"
			
			assert_equal(PointOfCompass::WEST, @rover.heading)
		end
		
		def test_execute_rotate_right_right_right_right_should_head_back_north
			@rover.execute! "R"
			@rover.execute! "R"
			@rover.execute! "R"
			@rover.execute! "R"
			
			assert_equal(PointOfCompass::NORTH, @rover.heading)
		end

	end
	
	class TestNasaRoverMoveForward < Test::Unit::TestCase

		include ThoughtWorksMarsRoversProblem
		
		def setup
			@initial_position = Vector[37, 19]
			@plateau_stub = MarsPlateauStub.new
		end
		
		def test_move_forward_heading_north_should_go_north
			heading = PointOfCompass::NORTH
			rover = NasaRover.new @initial_position, heading, @plateau_stub
			
			rover.execute! "M"
			
			assert_equal(@initial_position[0], rover.position[0])
			assert_equal(@initial_position[1] + 1, rover.position[1])
			assert_equal(heading, rover.heading)			
		end
		
		def test_move_forward_heading_south_should_go_south
			heading = PointOfCompass::SOUTH
			rover = NasaRover.new @initial_position, heading, @plateau_stub
			
			rover.execute! "M"
			
			assert_equal(@initial_position[0], rover.position[0])
			assert_equal(@initial_position[1] -1, rover.position[1])
			assert_equal(heading, rover.heading)			
		end

		def test_move_forward_heading_east_should_go_east
			heading = PointOfCompass::EAST
			rover = NasaRover.new @initial_position, heading, @plateau_stub
			
			rover.execute! "M"
			
			assert_equal(@initial_position[0] + 1, rover.position[0])
			assert_equal(@initial_position[1], rover.position[1])
			assert_equal(heading, rover.heading)			
		end

		def test_move_forward_heading_west_should_go_west
			heading = PointOfCompass::WEST
			rover = NasaRover.new @initial_position, heading, @plateau_stub
			
			rover.execute! "M"
			
			assert_equal(@initial_position[0] - 1, rover.position[0])
			assert_equal(@initial_position[1], rover.position[1])
			assert_equal(heading, rover.heading)			
		end

	end
	
	class TestNasaRoverPlateauOverstep < Test::Unit::TestCase

		include ThoughtWorksMarsRoversProblem
		
		def test_deploy_outside_the_plateau_should_raise_error
			initial_position = Vector[37, 19]
			any_initial_heading = PointOfCompass::NORTH
			
			plateau_stub = MarsPlateauStub.new
			plateau_stub.stub_include_coordinates? lambda { |coordinates| return false }
			
			assert_raise(NasaRover::OverstepPlateauError) { NasaRover.new initial_position,  any_initial_heading, plateau_stub }
		end
		
		def test_moving_outside_the_plateau_should_raise_error
			initial_position = Vector[37, 19]
			any_initial_heading = PointOfCompass::NORTH
			
			plateau_stub = MarsPlateauStub.new
			plateau_stub.stub_include_coordinates? lambda { |coordinates| return false if coordinates != initial_position; return true }
			rover = NasaRover.new initial_position, any_initial_heading, plateau_stub
			
			assert_nothing_raised { rover.execute! "L" }
			assert_nothing_raised { rover.execute! "R" }
			assert_raise(NasaRover::OverstepPlateauError) { rover.execute! "M" }
		end
	end
	
	class TestNasaRoverTakenSpotError  < Test::Unit::TestCase
		
		include ThoughtWorksMarsRoversProblem
		
		def test_deploy_over_existing_obstacle_should_raise_error
			initial_position = Vector[37, 19]
			obstacles = [initial_position]
			any_initial_heading = PointOfCompass::NORTH
			
			plateau_stub = MarsPlateauStub.new
			
			assert_raise(NasaRover::TakenSpotError) { NasaRover.new initial_position,  any_initial_heading, plateau_stub, obstacles }
		end
		
		def test_moving_over_an_existing_obstacle_should_raise_error
			initial_position = Vector[37, 19]
			initial_heading = PointOfCompass::NORTH
			obstacles = [initial_position + initial_heading]
			
			plateau_stub = MarsPlateauStub.new
			rover = NasaRover.new initial_position, initial_heading, plateau_stub, obstacles
			
			assert_raise(NasaRover::TakenSpotError) { rover.execute! "M" }
		end
		
	end
end
