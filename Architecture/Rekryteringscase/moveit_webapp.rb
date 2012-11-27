# encoding: UTF-8

require 'erb'
require 'sinatra'
require 'sinatra/reloader' if development?


require_relative 'offer'
require_relative 'customer'

require_relative 'persistence'

require_relative 'models/offer_model'
require_relative 'models/offers_list_model'
require_relative 'models/offer_request_model'

require_relative 'errors_handler'




include ERB::Util

include ErrorsHandler

# Session storage implemendet with Rack. Notes:
# - the cookie used is a persisten cookie because expiration date is set
# - the capacity of the session is limited to 4KB, storing bigger quantity cause
#   the session content to be lost
# - the secret is set so the integrity of the cookie content in verified
# - the cookie content is sent in clear (base64 encoded)
use Rack::Session::Cookie,	:key => 'rack.session',
					:path => '/',
					:expire_after => 2592000, #30 days in seconds
					:secret => 'random_noise_sdoigfposdafgksdgjopsdöagjosdaujg'


persistence = Persistence.new(File.join(File.expand_path(File.dirname(__FILE__)), "database"))


get '/' do
	redirect url("/offerrequest")
end


get '/offerrequest' do
	if is_customer_in_cookie
		customer = get_customer_from_cookie
		model = OfferRequestModel.new customer
	else
		model = OfferRequestModel.new		
	end
		
	erb :offer_request, :locals => { :model => model }
end


post '/offerrequest' do
	model = OfferRequestModel.from_params(params)
		
	if model.is_input_valid? == false		
		return erb :offer_request, :locals => { :model => model }		
	end
	
	if is_customer_in_cookie
		customer = get_customer_from_cookie
	else
		customer = Customer.new model.first_name, model.last_name, model.email
	end

	customer_id = customer.id 
	from = model.from
	to = model.to
	distance = Integer(model.distance)
	house_area = Integer(model.house_area)
	basement_garret_area = Integer(model.basement_garret_area)
	with_piano = model.piano == "true"
	offer = Offer.new customer_id, from, to, distance, house_area, basement_garret_area, with_piano

	if is_customer_in_cookie
		persistence.insert_offer_for_existing_customer offer
	else
		persistence.insert_offer_with_new_customer offer, customer	
		save_customer_in_cookie customer
	end

	redirect build_offer_url(offer.id)
end


get '/offer/:offer_id' do
	
	offer, customer = persistence.read_offer_with_customer params[:offer_id]

	save_customer_in_cookie customer
	
	model = OfferModel.new customer, offer, build_offer_url(offer.id)
	
	erb :offer, :locals => { :model => model }
end


get '/listoffers' do
	if is_customer_in_cookie
		customer = get_customer_from_cookie
		
		offers_id_list = persistence.read_customer_offers_id_list(customer)
		
		offers_list = offers_id_list.map do |offer_id|
			date_parts = offer_id.split("_")[1].split("-")
			user_friendly_id = date_parts[0..2].join("-") + " " + date_parts[3..4].join(":") + "." + date_parts[5]
			[user_friendly_id, build_offer_url(offer_id)]
		end

		model = OffersListModel.new customer, offers_list
	else
		model = OffersListModel.new
	end

	erb :offers_list, :locals => { :model => model }
end


get '/logout' do
	clear_customer_from_cookie()
	redirect url('/')
end




def build_offer_url offer_id
	return url("/offer/#{offer_id}")
end

def save_customer_in_cookie customer
	session[:customer_id] = customer.id
	session[:customer_first_name] = customer.first_name
	session[:customer_last_name] = customer.last_name
	session[:customer_email] = customer.email
end

def clear_customer_from_cookie 
	session_keys = [:customer_id, :customer_first_name, :customer_last_name, :customer_email]
	session_keys.each { |key| session[key] = nil; session.delete(key) }
end

def is_customer_in_cookie
	return session[:customer_id]
end
	
def get_customer_from_cookie
	return Customer.new session[:customer_first_name] , session[:customer_last_name] , session[:customer_email] , session[:customer_id] 
end

def get_customer_id_from_cookie
	return session[:customer_id] 
end