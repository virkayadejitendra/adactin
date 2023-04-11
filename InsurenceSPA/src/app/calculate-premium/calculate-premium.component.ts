import { Component, OnInit } from '@angular/core';
import { OccupationService } from '../_services/occupation.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PremiumService } from '../_services/premium.service';
import { PremiumData } from '../_models/PremiumData';
import { PremiumResult } from '../_models/PremiumResult';
@Component({
  selector: 'app-calculate-premium',
  templateUrl: './calculate-premium.component.html',
  styleUrls: ['./calculate-premium.component.css'],
})
export class CalculatePremiumComponent implements OnInit {
  constructor(
    private premiumService: PremiumService,
    private occupationService: OccupationService
  ) {
    this.calcutedPremium = {
      tpdPremium: 0,
      deathPremium: 0,
    };
    this.occupationList = this.premiumInputForm.valueChanges.subscribe(() => {
      this.onSubmit();
    });
  }

  premiumInputForm = new FormGroup({
    name: new FormControl('', Validators.required),
    dateOfBirth: new FormControl(new Date(), [
      Validators.required,
      this.maxAgeValidator(70),
    ]),
    occupation: new FormControl('', Validators.required),
    sumInsured: new FormControl(-1, [Validators.required, Validators.min(1)]),
  });

  occupationList: any;
  calcutedPremium: PremiumResult;
  calcultedAge: any;

  ngAfterViewInit():void{
    this.occupationService.getOccupations().subscribe((respose) => {
      this.occupationList = respose;
    });

  }

  ngOnInit(): void {
  }
  onSubmit() {
    if (this.premiumInputForm.valid) {
      let inputData: PremiumData = {
        sumInsured: this.premiumInputForm.controls.sumInsured.value,
        occupation: this.premiumInputForm.controls.occupation.value,
        age: this.calcultedAge,
      };
      this.premiumService.calculatePremium(inputData).subscribe((respose) => {
        this.calcutedPremium = respose;
      });
    }
  }
  maxAgeValidator(maxAge: number) {
    return (control: FormControl) => {
      const dateOfBirth = new Date(control.value);
      const age = this.calculateAge(dateOfBirth);
      this.calcultedAge = age;
      return age >= maxAge ? { ageTooHigh: true } : null;
    };
  }
  calculateAge(dateOfBirth: Date) {
    const ageDiffMs = Date.now() - dateOfBirth.getTime();
    const ageDate = new Date(ageDiffMs);
    const age = Math.abs(ageDate.getUTCFullYear() - 1970);
    return age;
  }
}
