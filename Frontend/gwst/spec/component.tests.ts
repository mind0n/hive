
angular.module('myComponentModule', [])
.component('myComponent', {
    bindings: {
        myBinding: '@'
    },
    controller: function() {
        this.myTitle = 'Unit Testing AngularJS';
    },
    template: '<h1>{{ $ctrl.myTitle }} {{ $ctrl.myBinding }}</h1>'
});

describe('Component: myComponent', function () {
    beforeEach(angular.mock.module('myComponentModule'));

    let element:any;
    let scope:any;
    let controller:any;
    beforeEach(inject(function($rootScope:any, $compile:any){
        scope = $rootScope.$new();
        element = angular.element('<my-component my-binding="{{outside}}"></my-component>');
        element = $compile(element)(scope);
        controller = element.controller("myComponent");
        scope.outside = '1.5';
        scope.$apply();
    }));

    it('should render the text', function() {
        var h1 = element.find('h1');
        expect(h1.text()).toBe('Unit Testing AngularJS 1.5');
    });
    
    it('should expose my title', function() {
        expect(controller.myTitle).toBeDefined();
        expect(controller.myTitle).toBe('Unit Testing AngularJS');
    });
});