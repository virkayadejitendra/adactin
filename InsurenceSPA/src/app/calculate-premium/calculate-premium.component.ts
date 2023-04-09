import { Component, OnInit } from '@angular/core';
import { OccupationService } from '../_services/occupation.service';
import { FormGroup, FormControl, Validators } from '@angular/forms'
@Component({
  selector: 'app-calculate-premium',
  templateUrl: './calculate-premium.component.html',
  styleUrls: ['./calculate-premium.component.css']
})
export class CalculatePremiumComponent implements OnInit {
  myForm = new FormGroup({
    name: new FormControl('', Validators.required),
    age: new FormControl('', Validators.required),
    dateOfBirth: new FormControl('', Validators.required),
    occupation: new FormControl('', Validators.required),
    sumInsured: new FormControl('', Validators.required)
  });

  
  occupationList:any;

  constructor(private service:OccupationService) {

    this.myForm.valueChanges .subscribe(() => {
      this.onSubmit();
    });
   }

  ngOnInit(): void {
    this.service.getOccupations().subscribe(
      respose=>{
        this.occupationList=respose
      }
    )
  }
 

  onSubmit(){
    if(this.myForm.valid)
      console.log("User data: ", this.myForm.value);
   

  }
}
