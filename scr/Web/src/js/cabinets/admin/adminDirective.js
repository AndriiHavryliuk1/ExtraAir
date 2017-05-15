app.directive("tableView", function() {
    return {
        restrict: 'A',
        templateUrl: function(element, attrs) {

            switch (attrs["tableView"]) {
                case "airportsView":
                    return "js/cabinets/admin/templates/airportsTemplate.html";
                    break;
                case "tourStatusView":
                    return "js/cabinets/admin/templates/tourStatusAdminTemplate.html";
                    break;
                case "registerEmployeeView":
                    return "src/views/templates/RegisterEmployeeTemplate.html";
                    break;
                case "employeeView":
                    return "src/views/templates/ViewEmployeeTemplate.html";
                    break;
                case "medicationsView":
                    return "src/views/templates/ViewMedicationsTemplate.html";
                    break;
                case "newDepartment":
                    return "src/views/templates/AddDepartmentTemplate.html";
                    break;
            }
        }
    }
});