export class Donor {
    name: string;
    idNumber: string; 
    dateOfBirth: Date; 

    constructor(name: string, id: string, dateOfBrith: Date) {
        this.name = name;
        this.idNumber = id; 
        this.dateOfBirth =  dateOfBrith; 
    }
}