import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-overall-ship-status-list',
  templateUrl: './overallstatusofship-list.component.html',
  styleUrls: ['./overallstatusofship-list.component.css']
})
export class OverallShipStatusListComponent implements OnInit {
  // Table data source
  dataSource = new MatTableDataSource<any>([]);
  
  // Paginator properties
  paging = {
    length: 0,
    pageSize: 10,
    pageSizeOptions: [5, 10, 25, 50]
  };

  // Master data for paginator options
  masterData = {
    paging: {
      showFirstLastButtons: true,
      pageSizeOptions: [5, 10, 25, 50]
    }
  };

  constructor() {}

  ngOnInit(): void {
    this.loadShipStatusData();
  }

  // Load initial data
  loadShipStatusData(): void {
    // Mock data loading or API call
    // Replace this with an actual service call to fetch ship status data
    this.dataSource.data = [];
    this.paging.length = this.dataSource.data.length;
  }

  // Apply filtering to the table
  applyFilter(filterValue: string): void {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  // Handle page change
  pageChanged(event: any): void {
    // Logic to fetch paginated data based on event.pageIndex and event.pageSize
  }

  // Print functionality
  printOverallShipStatus(): void {
    // Add logic for print functionality
    console.log('Print Overall Ship Status');
  }

  // Delete an item
  deleteItem(status: any): void {
    // Add logic to delete the selected item
    console.log('Delete Item:', status);
  }
}
