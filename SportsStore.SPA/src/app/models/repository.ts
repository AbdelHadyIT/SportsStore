import { Http, Request, RequestMethod } from '@angular/http';
import { Product } from './product.model';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { map, filter } from 'rxjs/operators';
import { Filter } from './configClasses.repository';
import { Supplier } from './supplier.model';

const productsUrl = 'http://localhost:5000/api/product';
const suppliersUrl = 'http://localhost:5000/api/supplier';

@Injectable({
    providedIn: 'root'
})
export class Repository {
    private filterObject = new Filter();
    product: Product;
    products: Product[];
    suppliers: Supplier[];

    get filter (): Filter {
        return this.filterObject;
    }

    constructor(private _http: Http) {
        this.filter.category = 'Watersports';
        this.filter.related = true;
        this.getProducts(true);
        this.getSupplier();
    }

    private sendRequest(verb: RequestMethod, url: string, data?: any): Observable<any> {
        return this._http.request(new Request({method: verb, url: url, body: data}))
        .pipe(map(response => response.json()));
    }

    getProduct(id: number): void {
        this._http.get(productsUrl + '/getProduct?id=' + id)
            .subscribe(response => {this.product = response.json();  });
    }

    getProducts(related: boolean ): void {
        let url = productsUrl + '?related=' + related ;
        if (this.filter.category) {
            url += '&category=' + this.filter.category;
        }
        if (this.filter.search) {
            url += '&search=' + this.filter.search;
        }
        this.sendRequest(RequestMethod.Get, url)
        .subscribe(response => {this.products = response; } );
    }

    getSupplier() {
        this.sendRequest(RequestMethod.Get, suppliersUrl)
        .subscribe(response => this.suppliers = response);
    }

    CreateProduct(product: Product) {
        const data = {
            name: product.name, category: product.category,
            description: product.description, price: product.price,
            supplier: product.supplier ? product.supplier.supplierId : 0
        };
        this.sendRequest(RequestMethod.Post, productsUrl + '/addProduct', data)
        .subscribe(response => {
            product.productId = response;
            this.products.push(product);
        });
    }

    createProductAndSupplier(product: Product, supplier: Supplier) {
        const data = {
            name: supplier.name,
            city: supplier.city,
            state: supplier.state
        };
        this.sendRequest(RequestMethod.Post, suppliersUrl + '/addSupplier', data)
        .subscribe(response => {
            supplier.supplierId = response;
            product.supplier = supplier;
            this.suppliers.push(supplier);
            if (product != null) {
                this.CreateProduct(product);
            }
        });
    }

}
