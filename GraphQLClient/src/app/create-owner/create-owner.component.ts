import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GraphqlService } from '../graphql/graphql.service';

@Component({
  selector: 'app-create-owner',
  templateUrl: './create-owner.component.html',
  styleUrls: ['./create-owner.component.css']
})
export class CreateOwnerComponent implements OnInit {
  createForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private service: GraphqlService,
              private router: Router) { 

  }

  ngOnInit() {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createForm.invalid) {
        return;
    }

    // display form values on success
    //alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.createForm.value, null, 4));

    this.service.createOwner(this.createForm.value).subscribe(data => {
      this.router.navigate(['/home']);
    });
  }

  onReset() {
    this.submitted = false;
    this.createForm.reset();
  }
}