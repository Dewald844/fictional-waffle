import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DxDataGridModule, DxButtonModule } from 'devextreme-angular';
import { exportDataGrid } from 'devextreme/excel_exporter';
import { Workbook } from 'exceljs';
import { CommonModule } from '@angular/common';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-olympic-grid',
  standalone: true,
  imports: [CommonModule, DxDataGridModule],
  templateUrl: './olympic-grid.component.html'
})
export class OlympicGridComponent implements OnInit {
  dataSource: any[] = [];

  constructor(private http: HttpClient) {}

  onExporting(e: any) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet('Olympic Winners');

    exportDataGrid({
      component: e.component,
      worksheet,
      autoFilterEnabled: true,
    }).then(() => {
      workbook.xlsx.writeBuffer().then((buffer) => {
        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'OlympicWinners.xlsx');
      });
    });

    e.cancel = true; // Prevent default export
  }

  ngOnInit() {
    this.http.get<any[]>('http://localhost:5210/olympic-winners')
      .subscribe(data => this.dataSource = data);
  }
}
