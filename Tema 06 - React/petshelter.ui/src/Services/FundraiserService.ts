import axios from 'axios';
import { Person } from '../Models/Person';
import { Fundraiser } from '../Models/Fundraiser';


export class FundraiserService {
    private apiUrl: string = "https://localhost:7075";

    public getAll(): Promise<Fundraiser[]> {
        return axios
        .get(this.apiUrl + '/Fundraiser/getAll')
        .then(response => {
            var fundraiserResponse: FundraiserDto[] = [];
            response.data.forEach((fundraiserFromApi: FundraiserDto) => {

                fundraiserResponse.push(
                    new Fundraiser( 
                    fundraiserFromApi.name, 
                    fundraiserFromApi.status, 
                    )
                )
            });
            return fundraiserResponse;
        })
    }  

    public  getTotalNumber(): Promise<number> {
         return   axios
        .get(this.apiUrl + '/Fundraiser/getAll')
        .then(response => {
            var c: number = 0; 
            response.data.forEach(() => { c = c + 1;});
            return c;
        })
    } 



    public getById(id: number):  Promise<Fundraiser> {
         return axios
        .get(this.apiUrl + '/Fundraiser/' + id ) 
        .then(response => {    
            var donorsResponse: PersonDto[] = [];
            response.data["donors"].forEach((donor: PersonDto) => {
                donorsResponse.push(
                    new Person( 
                        donor.name,  
                        donor.idNumber, 
                        donor.dateOfBirth  
                    )
                )
            }); 

            return new Fundraiser (
                response.data["name"],  
                response.data["status"],
                response.data["dueDate"],
                response.data["goalAmount"], 
                donorsResponse,  
                response.data["currentAmount"], 
            );
        })
    }  
    public createFundraiser(fundraiser: Fundraiser,ownerId: number ): Promise<void> {

    let newCreatedFundraiser: FundraiserCreateDto = {
        name: fundraiser.name,
        dueDate: fundraiser.dueDate, 
        goalAmount: fundraiser.goalAmount
    }; 
    return axios.post(this.apiUrl + '/Fundraiser/createFundraiser?OwnerId='+ownerId, newCreatedFundraiser); 
    }  

    public donateToFundraiser(amount: number, donorId: number, fundraiserId: number ): Promise<void> {

        let newCreatedFundraiser: FundraiserDonation = {
            amount,
            donorId,
            fundraiserId
        }; 
        return axios.post(this.apiUrl + '/Fundraiser/donateToFundraiser', newCreatedFundraiser); 
        }  

    public deleteFundraiser(fundraiserId: number ): Promise<void> {
            return axios.delete(this.apiUrl + '/Fundraiser/deleteFundraiser/'+fundraiserId); 
            } 

}

interface FundraiserDto
{
    name: string; 
    status?:string; 
}  
interface FundraiserDonation 
{
    amount: number; 
    donorId: number; 
    fundraiserId: number; 
} 

interface FundraiserCreateDto
{
    name: string;  
    dueDate?: Date; 
    goalAmount?: number; 
}



interface PersonDto
{
    name: string; 
    dateOfBirth: Date; 
    idNumber: string; 
}


