require 'test/unit'
require_relative '../mars_plateau'

module ThoughtWorksMarsRoversProblemUnitTest

	class TestMarsPlateauInitialize < Test::Unit::TestCase
		
		include ThoughtWorksMarsRoversProblem
	
		def test_a_negative_x_should_raise_an_exception
			assert_raise(MarsPlateau::InvalidCoordinatesError) do
				MarsPlateau.new Vector[-1, 1]
			end
		end
		
		def test_a_negative_y_should_raise_an_exception
			assert_raise(MarsPlateau::InvalidCoordinatesError) do
				MarsPlateau.new Vector[1, -1]
			end
		end
		
		def test_upper_right_corner_should_be_equal_to_the_argument
			upper_right_coordinates = Vector[5, 5]
			
			plateau = MarsPlateau.new upper_right_coordinates
			
			assert_equal(Vector[5, 5], plateau.upper_right_coordinates)			
		end

		def test_lower_left_corner_should_be_the_origin
			any_valid_upper_right_coordinates = Vector[5, 5]
			
			plateau = MarsPlateau.new any_valid_upper_right_coordinates
			
			assert_equal(Vector[0, 0], plateau.lower_left_coordinates)			
		end
	end

	class TestMarsPlateauInclude < Test::Unit::TestCase
		
		include ThoughtWorksMarsRoversProblem
	
		def setup
			upper_right_coordinates = Vector[15, 29]			
			@plateau = MarsPlateau.new upper_right_coordinates
		end

		def test_does_include_plateau_corners_coordinates			
			max_x = @plateau.upper_right_coordinates[0]
			max_y = @plateau.upper_right_coordinates[1]
			
			assert(@plateau.include_coordinates? @plateau.lower_left_coordinates)
			assert(@plateau.include_coordinates? @plateau.upper_right_coordinates)
			assert(@plateau.include_coordinates? Vector[0, max_y])
			assert(@plateau.include_coordinates? Vector[max_x, 0])
		end
		
		def test_does_include_an_internal_coordinate
			
			assert(@plateau.include_coordinates?(@plateau.lower_left_coordinates + Vector[1, 0]))
			assert(@plateau.include_coordinates?(@plateau.lower_left_coordinates + Vector[0, 1]))
			assert(@plateau.include_coordinates?(@plateau.upper_right_coordinates - Vector[1, 0]))
			assert(@plateau.include_coordinates?(@plateau.upper_right_coordinates - Vector[0, 1]))			
		end
		
		def test_does_not_include_an_external_coordinate
			
			assert(!@plateau.include_coordinates?(@plateau.lower_left_coordinates - Vector[1, 0]))
			assert(!@plateau.include_coordinates?(@plateau.lower_left_coordinates - Vector[0, 1]))
			assert(!@plateau.include_coordinates?(@plateau.upper_right_coordinates + Vector[1, 0]))
			assert(!@plateau.include_coordinates?(@plateau.upper_right_coordinates + Vector[0, 1]))			
		end
	end

end
