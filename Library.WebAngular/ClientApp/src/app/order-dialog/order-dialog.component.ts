import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Order } from '../models/order';
import { LoadService } from '../services/load.service';
import { NotificationService } from '../services/notification.service';

@Component({
    selector: 'app-order-dialog',
    templateUrl: './order-dialog.component.html',
    styleUrls: ['./order-dialog.component.css']
})
export class OrderDialogComponent implements OnInit {
    order: Order;
    count: number;

    shippingCost: number;

    smallCityDepartment: number[] = [4, 34, 16, 29, 51, 6];
    mediulCityDepartment: number[] = [3, 81, 143, 112, 107, 10, 11, 20, 22, 41];
    bigCityDepartment: number[] = [12, 61, 23, 53, 103, 188, 111, 99, 71, 105, 66, 13, 87, 91];

    readonly regions: RegionSippmentDepartment[] = [
        { region: 'Kyiv', departments: this.bigCityDepartment, cordinateNumber: -1.5 },
        { region: 'Kharkiv', departments: this.bigCityDepartment, cordinateNumber: 1 },
        { region: 'Odesa', departments: this.bigCityDepartment, cordinateNumber: -1.5 },
        { region: 'Dnipro', departments: this.bigCityDepartment, cordinateNumber: 1 },
        { region: 'Donetsk', departments: this.bigCityDepartment, cordinateNumber: 2 },
        { region: 'Zaporizhzhia', departments: this.bigCityDepartment, cordinateNumber: 1 },
        { region: 'Lviv', departments: this.bigCityDepartment, cordinateNumber: -4 },
        { region: 'Kryvyi Rih', departments: this.mediulCityDepartment, cordinateNumber: 0.5 },
        { region: 'Mykolaiv', departments: this.bigCityDepartment, cordinateNumber: 1 },
        { region: 'Mariupol', departments: this.mediulCityDepartment, cordinateNumber: 1.5 },
        { region: 'Luhansk', departments: this.mediulCityDepartment, cordinateNumber: 2.5 },
        { region: 'Sevastopol', departments: this.mediulCityDepartment, cordinateNumber: 2 },
        { region: 'Vinnytsia', departments: this.bigCityDepartment, cordinateNumber: -1.5 },
        { region: 'Makiivka', departments: this.mediulCityDepartment, cordinateNumber: 2 },
        { region: 'Simferopol', departments: this.bigCityDepartment, cordinateNumber: 2 },
        { region: 'Kherson', departments: this.mediulCityDepartment, cordinateNumber: 1.5 },
        { region: 'Poltava', departments: this.bigCityDepartment, cordinateNumber: 1 },
        { region: 'Chernihiv', departments: this.mediulCityDepartment, cordinateNumber: -2.5 },
        { region: 'Khmelnytskyi', departments: this.mediulCityDepartment, cordinateNumber: -2.5 },
        { region: 'Chernivtsi', departments: this.mediulCityDepartment, cordinateNumber: -3.5 },
        { region: 'Zhytomyr', departments: this.mediulCityDepartment, cordinateNumber: -2 },
        { region: 'Sumy', departments: this.mediulCityDepartment, cordinateNumber: 2 },
        { region: 'Rivne', departments: this.mediulCityDepartment, cordinateNumber: -2.5 },
        { region: 'Horlivka', departments: this.mediulCityDepartment, cordinateNumber: 2 },
        { region: 'Ivano-Frankivsk', departments: this.mediulCityDepartment, cordinateNumber: -3.5 },
        { region: 'Kamianske', departments: this.mediulCityDepartment, cordinateNumber: -3 },
        { region: 'Ternopil', departments: this.mediulCityDepartment, cordinateNumber: 1 },
        { region: 'Lutsk', departments: this.mediulCityDepartment, cordinateNumber: -4 },
        { region: 'Bila Tserkva', departments: this.mediulCityDepartment, cordinateNumber: -0.5 },
        { region: 'Kramatorsk', departments: this.mediulCityDepartment, cordinateNumber: 1.5 },
        { region: 'Melitopol', departments: this.mediulCityDepartment, cordinateNumber: 2 },
        { region: 'Nikopol', departments: this.smallCityDepartment, cordinateNumber: 1 },
        { region: 'Berdiansk', departments: this.smallCityDepartment, cordinateNumber: 2 },
        { region: 'Sloviansk', departments: this.smallCityDepartment, cordinateNumber: 2.5 },
        { region: 'Yevpatoria', departments: this.smallCityDepartment, cordinateNumber: 2.5 },
        { region: 'Alchevsk', departments: this.smallCityDepartment, cordinateNumber: 2 },
        { region: 'Brovary', departments: this.smallCityDepartment, cordinateNumber: -2 },
        { region: 'Pavlohrad', departments: this.smallCityDepartment, cordinateNumber: 1 },
        { region: 'Sieverodonetsk', departments: this.smallCityDepartment, cordinateNumber: 2 },
        { region: 'Kamianets-Podilskyi', departments: this.smallCityDepartment, cordinateNumber: -3.5 },
        { region: 'Lysychansk', departments: this.smallCityDepartment, cordinateNumber: -2 },
        { region: 'Cherkasy', departments: this.bigCityDepartment, cordinateNumber: 0 },
        { region: 'Kropyvnytskyi', departments: this.mediulCityDepartment, cordinateNumber: 0 },
        { region: 'Kremenchuk', departments: this.mediulCityDepartment, cordinateNumber: 0 }
    ];

    shipFromControl = new FormControl('', Validators.required);
    shipFromDepartmentControl = new FormControl('', Validators.required);

    shipToControl = new FormControl('', Validators.required);
    shipToDepartmentControl = new FormControl('', Validators.required);

    get isValidShipFrom(): boolean {
        return this.shipFromControl.valid;
    }
    get isValidShipFromDepartmentValue(): boolean {
        return this.shipFromDepartmentControl.valid;
    }

    get isValidShipTo(): boolean {
        return this.shipToControl.valid;
    }

    get shipFromValue(): RegionSippmentDepartment {
        return this.shipFromControl.value as any as RegionSippmentDepartment;
    }

    get shipToValue(): RegionSippmentDepartment {
        return this.shipToControl.value as any as RegionSippmentDepartment;
    }

    constructor(
        @Inject(MAT_DIALOG_DATA) dialogData: any,
        private loadService: LoadService,
        private notificationService: NotificationService
    ) {
        this.order = dialogData.order as Order;
        this.countBooks();
        this.shippingCost = 35;
    }

    ngOnInit() {
    }

    private countBooks(): void {
        this.count = this.order.bookName.split(')').length - 1;
    }

    shipToChange() {
        this.shipToDepartmentControl.patchValue(null);
        this.calculateSum();
    }

    shipFromChange() {
        this.shipFromDepartmentControl.patchValue(null);
        this.calculateSum();
    }

    calculateSum() {
        if (!this.isValidShipFrom || !this.isValidShipTo) {
            return;
        }
        this.notificationService.showInfo('Calculating...');

        this.shippingCost = 35;
        let range = 0;

        const shipFrom: RegionSippmentDepartment = this.shipFromValue;
        const shipTo: RegionSippmentDepartment = this.shipToValue;

        if (shipFrom.cordinateNumber >= 0 && shipTo.cordinateNumber >= 0) {
            range = shipFrom.cordinateNumber - shipTo.cordinateNumber > 0
                ? shipFrom.cordinateNumber - shipTo.cordinateNumber
                : shipTo.cordinateNumber - shipFrom.cordinateNumber;
        }

        if (shipFrom.cordinateNumber < 0 && shipTo.cordinateNumber < 0) {
            range = (shipFrom.cordinateNumber + shipTo.cordinateNumber) * -1;
        }

        if ((shipFrom.cordinateNumber <= 0 && shipTo.cordinateNumber > 0)
            || (shipFrom.cordinateNumber >= 0 && shipTo.cordinateNumber < 0)) {
            range = shipFrom.cordinateNumber + shipTo.cordinateNumber > 0
                ? shipFrom.cordinateNumber + shipTo.cordinateNumber
                : (shipFrom.cordinateNumber + shipTo.cordinateNumber) * -1;
        }

        setTimeout(() => {
            this.shippingCost += (range * 6) + (2 * this.count);
            this.notificationService.showSuccess('The shipping will cost ' + this.shippingCost + 'UAH');
        }, 2500);
    }
}

interface RegionSippmentDepartment {
    region: string;
    departments: number[];
    cordinateNumber: number;
}

