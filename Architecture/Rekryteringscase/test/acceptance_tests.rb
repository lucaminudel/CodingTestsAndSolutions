# encoding: UTF-8

require 'test/unit'
require "watir-webdriver"

require_relative 'moveit_app.rb'

class AcceptanceTest < Test::Unit::TestCase

	def setup
		@browser = Watir::Browser.new  :firefox
		
		@app = MoveItApp.new @browser
	end
	
	def teardown
		@browser.close
	end
	
	def scenarios_setup
		@app.common_menu.log_out
	end
	
	def test_scenarios
		scenarios = [
			:offer_request_page_mandatory_fields_errors,
			:offer_request_page_numeric_fields_validation_error,
			:offer_request_page_name_fields_validation_error,
			:offer_request_page_email_basic_validation_error,
			:offer_request_page_successful_fields_validation,
		
			:offer_page_calculates_correct_price,
			:offer_page_contains_customer_and_moving_info_and_offer_link,			
			:offers_list_page_contains_all_the_offers_from_the_returning_customer,
			
			:after_obtaining_an_offer_the_user_is_recognized_as_returning_customer,
			:after_a_valid_request_and_logout_the_user_is_no_more_recognized_as_returning_customer,
			:after_visiting_the_link_of_an_offer_the_user_become_recognized_as_returning_customer,
			:when_the_user_is_recognized_as_returning_customer_the_user_info_is_pre_filled]
			
		scenarios.each do |scenario|
			scenarios_setup
			method(scenario).call
		end
	end
		
	def offer_request_page_mandatory_fields_errors
		offer_request_page = @app.goto_offer_request_page
		landing_page = offer_request_page.send_offer_request_form

		assert_same(offer_request_page, landing_page)
		
		error_msg = offer_request_page.read_mandatory_fields_error_message
		
		assert(error_msg.include? "Flytt från")
		assert(error_msg.include? "Flytt till")
		assert(error_msg.include? "Avstånd")
		assert(error_msg.include? "Bostadens yta")
		assert(error_msg.include? "Vind/Källare")
		assert(error_msg.include? "Förnamn")
		assert(error_msg.include? "Efternamn")
		assert(error_msg.include? "E-postadress")
	end

	def offer_request_page_numeric_fields_validation_error
		offer_request_page = @app.goto_offer_request_page
		
		offer_request_page.fill_offer_request_form :distance => "-1", :house_area => "1.1", :basement_garret_area => "g"		
		landing_page = offer_request_page.send_offer_request_form
		
		assert_same(offer_request_page, landing_page)
		
		error_msg = offer_request_page.read_fields_content_error_message
		
		assert(error_msg.include? "Avstånd must be a positive number")
		assert(error_msg.include? "Bostadens yta must be a positive number")
		assert(error_msg.include? "Vind/Källare must be a positive number")
	end
	
	def offer_request_page_name_fields_validation_error
		offer_request_page = @app.goto_offer_request_page
		
		offer_request_page.fill_offer_request_form :first_name => "Luca.", :last_name => "Minudel2"
		landing_page = offer_request_page.send_offer_request_form
		
		assert_same(offer_request_page, landing_page)
		
		error_msg = offer_request_page.read_fields_content_error_message
		
		assert(error_msg.include? "Förnamn should containt only letters")
		assert(error_msg.include? "Efternamn should containt only letters")
	end

	def offer_request_page_email_basic_validation_error
		offer_request_page = @app.goto_offer_request_page
		
		offer_request_page.fill_offer_request_form :email => "@not_an_email.no.no"
		landing_page = offer_request_page.send_offer_request_form
		
		assert_same(offer_request_page, landing_page)

		error_msg = offer_request_page.read_fields_content_error_message
		
		assert(error_msg.include? "E-postadress should be a valid email address")
	end

	def offer_request_page_successful_fields_validation
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form MoveItApp::OfferRequestPage::AnyValidOfferRequest
		landing_page = offer_request_page.send_offer_request_form
		
		assert_equal("MoveItApp::OfferPage", landing_page.class.to_s)
	end

	def offer_page_calculates_correct_price
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form :from => "here", :to=> "there", 
								:distance => "75", :house_area => "95", :basement_garret_area => "0",
								:first_name => "Mätteö", :last_name => "D'Ålessio",
								:email => "name@dot.com"
		offer_page = offer_request_page.send_offer_request_form
		
		assert_equal("MoveItApp::OfferPage", offer_page.class.to_s)

		price_including_vat = offer_page.read_offer_price_including_vat
		price_excluding_vat = offer_page.read_offer_price_excluding_vat
		
		assert_equal("11200", price_excluding_vat)
		assert_equal("14000.0", price_including_vat)				
	end
	
	def offer_page_contains_customer_and_moving_info_and_offer_link
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form :from => "here", :to=> "there", 
								:distance => "75", :house_area => "95", :basement_garret_area => "0",
								:first_name => "Mätteö", :last_name => "D'Ålessio",
								:email => "name@dot.com"
		offer_page = offer_request_page.send_offer_request_form
		
		assert_equal("MoveItApp::OfferPage", offer_page.class.to_s)
		
		name, surname, email = offer_page.read_customer_info
		assert_equal("Mätteö" ,name)
		assert_equal("D'Ålessio" ,surname)
		assert_equal("name@dot.com" ,email)
		
		from, to = offer_page.read_moving_place
		assert_equal("here", from)
		assert_equal("there", to)
		
		distance, house_area, basement_garret_area, with_piano = offer_page.read_moving_details
		assert_equal("75", distance)
		assert_equal("95", house_area)
		assert_equal("0", basement_garret_area)
		assert_equal("Nej", with_piano)
		
		offer_link = offer_page.read_offer_link
		page_link = offer_page.url
		
		assert_equal(offer_link, page_link)
	end

	def offers_list_page_contains_all_the_offers_from_the_returning_customer
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form MoveItApp::OfferRequestPage::AnyValidOfferRequest
		offer_request_page.send_offer_request_form
		
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form MoveItApp::OfferRequestPage::AnyValidOfferRequestForReturningCustomer
		offer_request_page.send_offer_request_form
		
		offer_request_page = @app.goto_offer_request_page
		offer_request_page.fill_offer_request_form MoveItApp::OfferRequestPage::AnyValidOfferRequestForReturningCustomer
		offer_page = offer_request_page.send_offer_request_form
		
		landing_page = @app.common_menu.click_offers_list_menu_item
		
		assert_equal("MoveItApp::OffersListPage", landing_page.class.to_s)
		
		customers_stored_offers_count = landing_page.count_customers_stored_offers
		
		assert_equal(3, customers_stored_offers_count)
	end
	

	def after_obtaining_an_offer_the_user_is_recognized_as_returning_customer
		@app.send_a_valid_offer_request_form_for_a_new_customer
		
		assert_equal(true, @app.common_menu.is_returning_customer)		
	end

	def after_a_valid_request_and_logout_the_user_is_no_more_recognized_as_returning_customer
		offer_page = @app.send_a_valid_offer_request_form_for_a_new_customer

		new_offer_url = offer_page.read_offer_link
		
		@app.common_menu.log_out
		
		assert_equal(false, @app.common_menu.is_returning_customer)		
	end

	def after_visiting_the_link_of_an_offer_the_user_become_recognized_as_returning_customer
		offer_page = @app.send_a_valid_offer_request_form_for_a_new_customer
		new_offer_url = offer_page.read_offer_link		
		@app.common_menu.log_out
		
		@browser.goto new_offer_url
		
		assert_equal(true, @app.common_menu.is_returning_customer)		
	end
	
	

	def when_the_user_is_recognized_as_returning_customer_the_user_info_is_pre_filled
		@app.send_a_valid_offer_request_form_for_a_new_customer
		
		offer_request_page = @app.goto_offer_request_page
		
		name, surname, email = offer_request_page.read_customer_info
		assert(name.strip().size > 0)
		assert(surname.strip().size > 0)
		assert(email.strip().size > 0)
		
	end
		
end
