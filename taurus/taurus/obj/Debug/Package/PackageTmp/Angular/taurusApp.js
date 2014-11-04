//Global app for the application
var app = angular.module('taurusApp', ['ui.bootstrap', 'ngRoute', 'mgcrea.ngStrap']);

app.factory('sessionService', ['$http', function ($http) {
    return {
        set: function (key, value) {
            return sessionStorage.setItem(key, value);
        },
        get: function (key) {
            return sessionStorage.getItem(key);
        },
        destroy: function (key) {
            return sessionStorage.removeItem(key);
        }
    };
} ]);

app.config(function($datepickerProvider) {
  angular.extend($datepickerProvider.defaults, {
    dateFormat: 'dd/MM/yyyy',
    startWeek: 1,
    autoclose: true
  });
})


app.factory('alertService', ['$rootScope', function($rootScope) {
	var alertService;
	$rootScope.systemAlerts = [];
	return alertService = {
	    addAlert: function (type, msg) {
		return $rootScope.systemAlerts.push({type: type, msg: msg,	close: function() {
			return alertService.closeAlert(this);
		  }
		});
	  },
	  closeAlert: function(alert) {
	      return this.closeAlertIdx($rootScope.systemAlerts.indexOf(alert));
	  },
	  closeAlertIdx: function(index) {
	      return $rootScope.systemAlerts.splice(index, 1);
	  },
	  clear: function(){
	      $rootScope.systemAlerts = [];
	  }
	};
  }
]);

