# encoding: UTF-8


set :raise_errors, Proc.new { false }
set :show_exceptions, false


module ErrorsHandler

	not_found do
		log_error_to_file("page_not_found_")
		erb :imlost
	end

	error do
		log_error_to_file()
		erb :imadeamistake		
	end
	
	private
	def log_error_to_file filename_prefix = "", username = get_customer_id_from_cookie() || "anonymous"
		sinatra_error = request.env["sinatra.error"]
		sinatra_html_error_page_builder = Sinatra::ShowExceptions.new(nil)
		

		now = Time.now
		now_formatted = now.strftime("%Y_%m_%d_%H_%M_%S_") << "%06d" % now.usec
		filename = "#{filename_prefix}#{now_formatted}_#{username}.html"
		
		path = File.join(File.expand_path(File.dirname(__FILE__)), "errors")

		File::open(File.join(path, filename), "w") do |file|			
			file.write sinatra_html_error_page_builder.pretty(request.env, sinatra_error)[0]
		end			
	end

end