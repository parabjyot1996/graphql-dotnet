import { Component, OnInit, Query } from '@angular/core';
import { Router } from '@angular/router';

import { GraphqlService } from '../graphql/graphql.service';
import { OwnerType } from '../types/ownerType';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public owners: OwnerType[];

  constructor(private service: GraphqlService,
              private router: Router) {
  }

  ngOnInit() {
    console.log("Before service call");
    this.getOwners();
  }

  getOwners() {
    this.service.getAllOwners().subscribe(data => {
      console.log(data.owners);
      this.owners = data.owners;
    });
  }
}