import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { OccupationService } from './occupation.service';
import { environment } from 'src/environments/environment';

describe('OccupationService', () => {
  let service: OccupationService;
  let httpMock: HttpTestingController;
  const apiUrl = environment.apiUrl;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OccupationService]
    });
    service = TestBed.inject(OccupationService);
    httpMock = TestBed.inject(HttpTestingController);
  });
  afterEach(() => {
    httpMock.verify();
  });
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call API endpoint to get a list of occupations', () => {
    const mockOccupations = [{ id: 1, name: 'Doctor' }, { id: 2, name: 'Farmer' }];
    service.getOccupations().subscribe((data: any) => {
      expect(data).toEqual(mockOccupations);
    });
    const req = httpMock.expectOne(apiUrl + 'Occupation');
    expect(req.request.method).toBe('GET');
    req.flush(mockOccupations);
  });
});
