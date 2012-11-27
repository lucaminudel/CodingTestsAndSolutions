# encoding: UTF-8

class MoveItApp
	
	BaseUrl = 'http://localhost:4567/'
	OfferRequestPageUrl = 'offerrequest'

	class BasePage
		def initialize browser
			@browser = browser		
		end
	end

	class ReturningCustomerMenuPartial < BasePage

		def is_returning_customer
			logout_link = @browser.link :id => "logoutLink"
			return logout_link.exists?					
		end
		
		def click_offers_list_menu_item
			link = @browser.link :id => "offers_list"
			link.click
			
			return OffersListPage.new(@browser)
		end

		def log_out
			@browser.goto BaseUrl
			
			logout_link = @browser.link :id => "logoutLink"
			logout_link.click if logout_link.exists?		
		end		
	end
	
	class OfferRequestPage < BasePage
		
		AnyValidOfferRequest = {
			:from => "here", :to=> "there", 
			:distance => "75", :house_area => "95", :basement_garret_area => "0",
			:first_name => "Mätteö", :last_name => "D'Ålessio",
			:email => "name@dot.com"}
		AnyValidOfferRequestForReturningCustomer = {
			:from => "here", :to=> "there", 
			:distance => "75", :house_area => "95", :basement_garret_area => "0"}
		
		def initialize browser, base_url, page_path
			@base_url = base_url
			@page_path = page_path
			
			super(browser)
		end
		
		def go
			@browser.goto @base_url + @page_path
			return self
		end
		
		def fill_offer_request_form input
			input.each do |text_field_id, input_value|
				field = @browser.text_field :id => text_field_id.to_s 
				field.set input_value
			end
		end
		
		def send_offer_request_form
			send_link = @browser.link :id => "sendRequest"
			send_link.click
			
			if @browser.url.include?(@page_path)
				return self
			else
				return OfferPage.new @browser
			end
		end
		
		def read_mandatory_fields_error_message
			mandatory_errors_div = @browser.div :id => "mandatoryErrorMessageText"			
			return mandatory_errors_div.text		
		end		
		
		def read_fields_content_error_message
			content_errors_div = @browser.div :id => "contentErrorMessageText"
			return content_errors_div.text
		end
		
		def read_customer_info
			return get_text_field_content("first_name"), get_text_field_content("last_name"), get_text_field_content("email")
		end
		
private
		def get_text_field_content id
			text_field = @browser.text_field :id => id
			return text_field.value
		end		
	end

	class OfferPage < BasePage
		
		def read_offer_price_including_vat
			get_span_text_content "price_including_vat"
		end
		
		def read_offer_price_excluding_vat
			get_span_text_content "price"
		end
		
		def read_customer_info
			return get_span_text_content("first_name"), get_span_text_content("last_name"), get_span_text_content("email")
		end
		
		def read_moving_place
			return get_span_text_content("from"), get_span_text_content("to")
		end
		
		def read_moving_details
			return get_span_text_content("distance"), get_span_text_content("house_area"), 
				  get_span_text_content("basement_garret_area"), get_span_text_content("with_piano")
		end
			  
		def read_offer_link
			link = @browser.link :id => "offer_url"
			return link.href
		end
		
		def url
			@browser.url
		end
				
private
		def get_span_text_content id
			span = @browser.span :id => id
			return span.text
		end
	end
	
	class OffersListPage  < BasePage
		
		def count_customers_stored_offers
			offers_list = @browser.ul :id => "offers_list"
			return offers_list.links.size
		end
	end



	def initialize browser
		@browser = browser		
	end
	
	def goto_offer_request_page
		offer_request_page = OfferRequestPage.new @browser, BaseUrl, OfferRequestPageUrl		
		offer_request_page.go
	end

	def common_menu
		return ReturningCustomerMenuPartial.new @browser
	end
	
	def send_a_valid_offer_request_form_for_a_new_customer
		common_menu.log_out
		
		offer_request_page = goto_offer_request_page
		offer_request_page.fill_offer_request_form MoveItApp::OfferRequestPage::AnyValidOfferRequest
		offer_page = offer_request_page.send_offer_request_form
		
		return offer_page
	end
	
end
