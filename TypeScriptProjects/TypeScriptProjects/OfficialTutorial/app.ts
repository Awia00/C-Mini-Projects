class Student {
    fullname: string;
    constructor(public firstname, public middleinitial, public lastname) {
        this.fullname = firstname + " " + middleinitial + " " + lastname;
    }
}

interface IPerson {
    firstname: string,
    lastname: string;
}
function greeter2(person: IPerson) {
    return `Hello ${person.firstname} ${person.lastname}`;
}

var user = new Student("Jane", "M.", "User");

window.onload = () => {
    document.body.innerHTML = greeter2(user);
    document.body.innerHTML = user.middleinitial;
};



