(function() {

    'use strict';


////Initializing the main module////
    
    angular.module('waitlist', ['ngResource']);


////Setting up the SignIn $resource factory: "SignIn"////
////SignIn now can GET, POST, SAVE, REMOVE, and DELETE////

    angular.module('waitlist').factory('SignIn', function($resource) {
	return $resource('/signin/new/:id');
    });

    

    angular.module('waitlist').controller('ResourceController', function($scope, SignIn) {
	
    })

})();