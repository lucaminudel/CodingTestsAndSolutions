require 'matrix'

module ThoughtWorksMarsRoversProblem

	module PointOfCompass
		N = NORTH = Vector[0, +1]
		S = SOUTH = Vector[0, -1]
		E = EAST = Vector[+1, 0]
		W = WEST = Vector[-1, 0]
		
		def self.to_s(point)
			return {NORTH => "N", SOUTH => "S", EAST => "E", WEST => "W"}[point]
		end
	end
	
end