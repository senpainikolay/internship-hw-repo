import type { Donor } from "./Donor";

export  class Fundraiser {
  
name:	string;
dueDate?: Date; 
goalAmount?:	number;
status?:	string;
donors?:   Donor[]; 
currentAmount?:	number;  

constructor(name: string, status?: string,  dueDate?: Date, goalAmount?: number,  donors?: Donor[], currentAmount?: number ) {  
    this.name = name; 
    this.dueDate = dueDate; 
    this.goalAmount = goalAmount;
    this.donors = donors;  
    this.status = status; 
    this.currentAmount = currentAmount; 

}
}