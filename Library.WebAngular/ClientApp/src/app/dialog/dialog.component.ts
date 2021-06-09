import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Author } from '../models/author';

@Component({
    selector: 'app-dialog',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.css']
})

export class DialogComponent implements OnInit {
    author: Author;
    descriptioNumber: number;
    randNumber: number;
    boolValues: boolean[] = [false, false, false, false, false];

    constructor(
        @Inject(MAT_DIALOG_DATA) dialogData: any
    ) {
        this.author = dialogData.author;
        this.descriptioNumber = dialogData.indexNumber;
        this.randNumber = Math.floor(Math.random() * 4) + 1;
    }

    ngOnInit() {
        this.boolValues[this.randNumber] = true;
    }
}
