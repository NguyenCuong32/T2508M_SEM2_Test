package org.example.letriphuong.models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class National {
    private int nationalId;
    private String nationalName;

    @Override
    public String toString() {
        return nationalName;
    }
}
