//Global app for the application
var app = angular.module('taurusApp', ['ui.bootstrap', 'ngRoute', 'mgcrea.ngStrap']);

app.factory('arrayService', [function () {
    var _minLength = 3;
    return {
        filterBy: function (input, parameters) {
            if (input == null || (parameters == null || parameters.length <= 0)) {
                return input;
            }

            var filters = [];
            var filtered = []
            angular.forEach(parameters, function (val, key) {
                filters.push(key);
            });
            var match = false;
            for (var i = 0; i < input.length; i++) {
                for (var s = 0; s < filters.length; s++) {
                    var _property = parameters[filters[s]];
                    if (_property.length >= _minLength) {
                        if (typeof input[i][filters[s]] === 'string' && _property.trim() != '') {
                            match = match || (input[i][filters[s]].toLowerCase().indexOf(_property.toLowerCase()) >= 0);
                        }
                    }
                }

                if (match) {
                    filtered.push(input[i]);
                }
                match = false;
            }
            return filtered;
        }
    };
} ]);

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

