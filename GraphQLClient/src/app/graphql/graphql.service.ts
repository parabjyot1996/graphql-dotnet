import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { HttpLink } from 'apollo-angular-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import gql from 'graphql-tag';
import { Observable } from 'rxjs';
import {map} from 'rxjs/operators';
import { OwnerInputType } from '../types/ownerInputType';
 
@Injectable({
    providedIn: 'root'
})
export class GraphqlService {

    constructor(private apollo: Apollo, httpLink: HttpLink) {
        apollo.create({
        link: httpLink.create({ uri: 'http://localhost:5000/graphql' }),
        cache: new InMemoryCache()
        })
    }

    public getAllOwners(): Observable<any> {
        const getOwners = gql`query getOwners {
            owners {
            id
            name
            address
            }
        }
        `;

        return this.apollo
            .watchQuery({query: getOwners})
            .valueChanges.pipe(map(({data}) => data));
    }

    public createOwner(ownerToCreate: OwnerInputType): Observable<any> {
        console.log(ownerToCreate);

        const queryMutation = gql`mutation($owner: OwnerInput!) {
            createOwner(owner: $owner) {
                id
                name
                address
            }
        }`;

        return this.apollo
            .mutate({
                mutation: queryMutation,
                variables: {
                    owner: ownerToCreate
                }
            })
            .pipe(map(({data}) => data))
            .pipe(map(({error}) => error))
    }
}