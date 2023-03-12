import type { Person } from "./Person";

export  class Fundraiser {
  
name:	string;
dueDate?: Date; 
goalAmount?:	number;
status?:	string;
donors?:   Person[]; 
currentAmount?:	number;  

constructor(name: string, status?: string,  dueDate?: Date, goalAmount?: number,  donors?: Person[], currentAmount?: number ) {  
    this.name = name; 
    this.dueDate = dueDate; 
    this.goalAmount = goalAmount;
    this.donors = donors;  
    this.status = status; 
    this.currentAmount = currentAmount; 

}
}