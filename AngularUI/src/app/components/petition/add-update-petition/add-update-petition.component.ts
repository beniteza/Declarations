import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { PageLoadingService } from 'src/app/services/page-loading.service';
import { PetitionService } from 'src/app/services/petition.service';

@Component({
  selector: 'app-add-update-petition',
  templateUrl: './add-update-petition.component.html',
  styleUrls: ['./add-update-petition.component.css']
})
export class AddUpdatePetitionComponent implements OnInit {

  formModel: FormGroup;
  petitionId: number;

  constructor(private fb: FormBuilder, private router: Router, private service: PetitionService, private activatedRoute: ActivatedRoute, private notifier: NotifierService, private loading: PageLoadingService) { }

  ngOnInit(): void {
    if(this.router.url.includes('/update/')){
      const routeParams = this.activatedRoute.snapshot.paramMap;
      this.petitionId = Number(routeParams.get('id'));
      this.loadPetition(this.petitionId);
    }
    else{
      this.formModel = this.fb.group({
        Id: [null],
        Title: ['', Validators.required],
        Description: ['', Validators.required],
        Topic: ['', Validators.required],
        PetitionDate: ['', Validators.required],
        Country: ['', Validators.required],
        City: ['', Validators.required]
      });
    }
  }

  loadPetition(petitionId){

    this.loading.showLoading(true);

    this.service.get(petitionId).subscribe(
      (res: any) => {
        this.formModel = this.fb.group({
          Id: [res.id],
          Title: [res.title, Validators.required],
          Description: [res.description, Validators.required],
          Topic: [res.topic, Validators.required],
          PetitionDate: [formatDate(new Date(res.petitionDate), 'yyyy-MM-dd', 'en'), Validators.required],
          Country: [res.country, Validators.required],
          City: [res.city, Validators.required]
        });
        
        this.loading.showLoading(false);
      },
      err => {
        this.loading.showLoading(false);
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      },
    )
  }

  onSubmit(){
    if(this.petitionId == null ){
      this.service.add(this.formModel.value).subscribe(
        (res: any) => {
          this.notifier.notify('success', 'Save successful');
          this.router.navigateByUrl('/petition/' + res);
        },
        err => {
          this.notifier.notify('error', 'Error: Something went wrong!');
          console.log(err);
        }
      );
    }
    else{
      this.service.update(this.formModel.value).subscribe(
        (res: any) => {
          this.notifier.notify('success', 'Save successful');
        },
        err => {
          this.notifier.notify('error', 'Error: Something went wrong!');
          console.log(err);
        }
      );
    }
  }

}
