var Student = (function () {
    function Student(firstname, middleinitial, lastname) {
        this.firstname = firstname;
        this.middleinitial = middleinitial;
        this.lastname = lastname;
        this.fullname = firstname + " " + middleinitial + " " + lastname;
    }
    return Student;
})();
function greeter2(person) {
    return "Hello " + person.firstname + " " + person.lastname;
}
var user = new Student("Jane", "M.", "User");
window.onload = function () {
    document.body.innerHTML = greeter2(user);
    document.body.innerHTML = user.middleinitial;
};
//# sourceMappingURL=app.js.map