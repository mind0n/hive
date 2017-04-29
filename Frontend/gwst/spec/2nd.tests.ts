
angular.module('app', [])
.controller('PasswordController', function PasswordController($scope:any) {
  $scope.password = '';
  $scope.grade = function() {
    var size = $scope.password.length;
    if (size > 8) {
      $scope.strength = 'strong';
    } else if (size > 3) {
      $scope.strength = 'medium';
    } else {
      $scope.strength = 'weak';
    }
  };
});

describe("Angular Unit Test", function(){
    it("angular should exist", function(){
        expect(angular).toBeTruthy();    
    });
    it("angular module should exist", function(){
        expect(angular.module).toBeTruthy();    
    });

    beforeEach(angular.mock.module('app'));
    let $controller:any;
    beforeEach(inject(function(_$controller_:any){
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    it("Password should be strong", function(){
      let $scope:any = {};
      var controller = $controller('PasswordController', { $scope: $scope });
      $scope.password = 'longerthaneightchars';
      $scope.grade();
      expect($scope.strength).toEqual('strong');    
    });
});