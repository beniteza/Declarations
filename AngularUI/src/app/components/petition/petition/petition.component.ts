import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { PetitionService } from 'src/app/services/petition.service';

@Component({
  selector: 'app-petition',
  templateUrl: './petition.component.html',
  styleUrls: ['./petition.component.css']
})
export class PetitionComponent implements OnInit {

  petition: any;
  formModel: FormGroup;
  p: number = 1;

  constructor(private fb: FormBuilder, private router: Router, private service: PetitionService, private activatedRoute: ActivatedRoute, private notifier: NotifierService) { }

  ngOnInit(): void {
    // Get param from route.
    const routeParams = this.activatedRoute.snapshot.paramMap;
    const petitionId = Number(routeParams.get('id'));

    this.service.get(petitionId).subscribe(
      (res: any) => {
        this.petition = res;
      },
      err => {
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      },
    );

    this.formModel = this.fb.group({
      PetitionId: [petitionId],
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', Validators.required],
      Message: ['', Validators.required]
    });
  }

  onSubmit(){
    this.service.addSignature(this.formModel.value).subscribe(
      (res: any) => {
        this.formModel.reset();
        this.petition.signatureList.unshift(res);
        this.notifier.notify('success', 'Add successful');
      },
      err => {
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      }
    );
  }

}
