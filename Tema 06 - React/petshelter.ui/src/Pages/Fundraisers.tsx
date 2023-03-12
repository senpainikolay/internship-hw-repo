import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment } from "react"; 
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FundraiserCard } from "../Components/FundraiserCard";
import { Fundraiser } from "../Models/Fundraiser";
import { FundraiserService } from "../Services/FundraiserService";

export const Fundraisers =  () => {

    const fundraiserService = new FundraiserService(); 
    const [fundraisers, setFundraisers] = useState<Fundraiser[]>([]); 


    async function getFundraisers(): Promise<Fundraiser[]> {  
        let fundraisers: Fundraiser[] = [];     
        const result = await fundraiserService.getAll();
        for (let i = 1; i <= result.length; i++) {    
          const res = await fundraiserService.getById(i);
          fundraisers.push(res);
        }
        return fundraisers; 
    }

    useEffect(() => {
        async function fetchFundraisers() {
          const fetchedFundraisers = await getFundraisers();
          setFundraisers(fetchedFundraisers);
        }
        fetchFundraisers();
      }, []); 

      


    const handleDonate = (fundraiser: Fundraiser) => {
        console.log("Someone wants to donate " + fundraiser.name);
    }  

  

    return (
        <Fragment>
            <Box>
                <Button>
                    <Link to="/">Go to the home page</Link>
                </Button>
            </Box>
            <Container> 
                <Grid container spacing={4}>  
                {   
                 fundraisers.map((fundraiser,index) => ( 
                <Grid item key={index} xs={12} sm={6} md={4}>
                    <FundraiserCard fundraiser={fundraiser} handleDonate={() => handleDonate(fundraiser)}></FundraiserCard>
                </Grid>
                 ))
                        
                }
                  
                </Grid>
            </Container>
        </Fragment>
    );
}