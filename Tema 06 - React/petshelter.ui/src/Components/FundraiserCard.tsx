import { Button, Card, CardActions, CardContent, CardMedia,  Typography } from "@mui/material";
import { Fundraiser } from "../Models/Fundraiser";

export interface IFundraiserCardProps {
    fundraiser: Fundraiser;
    handleDonate: any;
}

export const FundraiserCard = (props: IFundraiserCardProps) => { 

    return (
        <Card sx={{ maxWidth: 345 }}> 
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
          {props.fundraiser.name}
          </Typography>
          <Typography variant="body2" color="text.secondary">  
          The goal is {props.fundraiser.goalAmount}. People have donated {props.fundraiser.currentAmount}. 
          {
            props.fundraiser.status === "Active" ? "You can still donate untill " + props.fundraiser.dueDate : "Unfortunally, the fundraiser is closed to donations!"

        }

          </Typography>
        </CardContent>
        <CardActions>
        {
            props.fundraiser.status === "Closed"
                ? null
                : <Button size="small" color='primary' variant="contained" onClick={props.handleDonate}>Donate!</Button>
        }
        </CardActions>
      </Card>
    );
}