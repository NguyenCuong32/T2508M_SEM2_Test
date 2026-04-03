package org.fptaptecht2508m.herogames.entity;

public class National {
    private int NationalId;
    private String NationalName;

    public National() {}

    public National(String NationalName){
        this.NationalName = NationalName;
    }

    public int getNationalId() {
        return NationalId;
    }

    public void setNationalId(int nationalId) {
        this.NationalId = nationalId;
    }

    public String getNationalName() {
        return NationalName;
    }

    public void setNationalName(String nationalName) {
        this.NationalName = nationalName;
    }
}
