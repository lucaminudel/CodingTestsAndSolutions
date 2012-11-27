
function CreateOfferRequestController() {
		
	var newObj = {
		
		doPostBack: function() {			
			var self = this;
			document.getElementById("ErrorMessage").style.display = "none";
			
			var mandatoryFieldsErrors = [];			
			[
				["from", "Flytt från"], ["to", "Flytt till"], ["distance", "Avstånd"], 
				["basement_garret_area", "Vind/Källare"], ["house_area", "Bostadens yta"], 
				["first_name", "Förnamn"], ["last_name", "Efternamn"], ["email", "E-postadress"]
			].forEach(function(element) {				
				mandatoryFieldsErrors = mandatoryFieldsErrors.concat( self._validateMandatoryField(element[0], element[1]) );				
			});
			
			
			var fieldsContentErrors = [];
			var positiveNumberRegEx = /^\d*$/;
			var validNameRegEx = /^[A-Za-zÀÈÌÒÙàèìòùÁÉÍÓÚáéíóúÂÊÎÔÛâêîôûÃÑÕãñõÄËÏÖÜŸäëïöüŸçÇŒœßØøÅå ']*$/;
			var simpleEmailRegEx = /\S+@\S+\.\S+/;
			[
				["distance", positiveNumberRegEx, "Avstånd must be a positive number."], 
				["basement_garret_area", positiveNumberRegEx, "Vind/Källare  must be a positive number."], 
				["house_area", positiveNumberRegEx, "Bostadens yta  must be a positive number."], 
				["first_name", validNameRegEx, "Förnamn should containt only letters."], 
				["last_name", validNameRegEx, "Efternamn should containt only letters."], 
				["email", simpleEmailRegEx, "E-postadress should be a valid email address."]
			].forEach(function(element) {				
				fieldsContentErrors = fieldsContentErrors.concat( self._validateFieldContent(element[0], element[1], element[2]) );				
			});	

			var formIsValid = (fieldsContentErrors.length + mandatoryFieldsErrors.length <= 0);
			if (formIsValid) {
				var theForm = document.forms['editForm'];
				theForm.submit();
				return true;
			}

			var mandatoryFieldsErrorsMessage = "";
			if (mandatoryFieldsErrors.length > 0) {
				mandatoryFieldsErrorsMessage = (mandatoryFieldsErrors.length > 1 ? "Fyll i alla obligatoriska fält: " : "Fyll i obligatoriskt fältet: ") + mandatoryFieldsErrors.join(", ") + ".";
			}
			
			var fieldsContentErrorsMessage = fieldsContentErrors.join("<br>");

			document.getElementById("ErrorMessage").style.display = "block";
			document.getElementById("mandatoryErrorMessageText").innerHTML = mandatoryFieldsErrorsMessage;
			document.getElementById("contentErrorMessageText").innerHTML = fieldsContentErrorsMessage;

			return false;					
		},
		
		_validateFieldContent: function(id, regExp, errorMessage) {
			var self = this;
			var text = document.getElementById(id).value.trim();
			if (self._isEmpty(id) === false && regExp.test(text) === false) {
				return [errorMessage];
			} 
			
			return [];
		},
		
		_validateMandatoryField: function(id, name) {
			var self = this;
			if (self._isEmpty(id)) {
				return [name];
			} 
			
			return [];						
		},
		
		_isEmpty: function(id) {
			return (document.getElementById(id).value.trim() === "");
		}
	};
	
	return newObj;
}
