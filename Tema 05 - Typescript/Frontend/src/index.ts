import { Donor } from './models/Donor';
import { Fundraiser } from './models/Fundraiser';
import { FundraiserService } from './services/FundraiserService';

let service = new FundraiserService();

let fundraiser = new Fundraiser( 
    "Fundraiser TS", 
    undefined, 
    new Date(2024,2,2), 
    100,
    undefined,
    undefined
)
//service.createFundraiser(fundraiser,1) 
service.getById(4).then( fundraiser => console.log(fundraiser)); 
service.donateToFundraiser(100,1,4) 
service.deleteFundraiser(4)
service.getAll().then( fundraiser => console.log(fundraiser)); 
