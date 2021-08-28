var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@angular/core';
let ProductEditComponent = class ProductEditComponent {
    constructor(dataService, router, activeRoute) {
        this.dataService = dataService;
        this.router = router;
        this.loaded = false;
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }
    ngOnInit() {
        if (this.id) {
            this.dataService.getProduct(this.id).subscribe((data) => {
                this.product = data;
                if (this.product != null)
                    this.loaded = true;
            });
        }
    }
    save() {
        this.dataService.updateProduct(this.product).subscribe(data => this.router.navigateByUrl("/"));
    }
};
ProductEditComponent = __decorate([
    Component({
        templateUrl: './product-edit.component.html'
    })
], ProductEditComponent);
export { ProductEditComponent };
//# sourceMappingURL=product-edit.component.js.map