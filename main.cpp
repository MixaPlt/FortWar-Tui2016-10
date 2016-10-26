#include <iostream>
#include <cstdio>
#include <stdio.h>
long long M, N, Turns, Mode;
long long m1, n1, m2, n2, Field[51][51];
long long Mount, mount[300][3], Water, water[300][3];
long long turns[300][3], rec[100000], Rec;
struct Cost
{
    long long fi;
    long long se;
    long long zn;
};
Cost cost[300000];
using namespace std;
// 0-empty, 1-empty1, 2-empty2, 3-Cap1, 4-Cap2, 5-fort1, 6-fort2, 7-water, 8-water1, 9-water2, 10-mount, 11-mount1, 12-mount2
void Paint(int color, int x1, int y1)
{
        Field[x1][y1] = 5+color-1;

        if(Field[x1][y1-1] < 3) Field[x1][y1-1] = 1+color-1;
        else
        if(Field[x1][y1-1] > 4 || Field[x1][y1-1] < 7) Field[x1][y1-1] = 5+color-1;
        else
        if(Field[x1][y1-1] > 6 || Field[x1][y1-1] < 10) Field[x1][y1-1] = 8+color-1;
        else
        Field[x1][y1-1] = 11+color-1;

        if(Field[x1-1][y1] < 3) Field[x1-1][y1] = 1+color-1;
        else
        if(Field[x1-1][y1] > 4 || Field[x1-1][y1] < 7) Field[x1-1][y1] = 5+color-1;
        else
        if(Field[x1-1][y1] > 6 || Field[x1-1][y1] < 10) Field[x1-1][y1] = 8+color-1;
        else
        Field[x1-1][y1] = 11+color-1;

        if(Field[x1][y1+1] < 3) Field[x1][y1+1] = 1+color-1;
        else
        if(Field[x1][y1+1] > 4 || Field[x1][y1+1] < 7) Field[x1][y1+1] = 5+color-1;
        else
        if(Field[x1][y1+1] > 6 || Field[x1][y1+1] < 10) Field[x1][y1+1] = 8+color-1;
        else
        Field[x1][y1+1] = 11+color-1;

        if(Field[x1+1][y1] < 3) Field[x1+1][y1] = 1+color-1;
        else
        if(Field[x1+1][y1] > 4 || Field[x1+1][y1] < 7) Field[x1+1][y1] = 5+color-1;
        else
        if(Field[x1+1][y1] > 6 || Field[x1+1][y1] < 10) Field[x1+1][y1] = 8+color-1;
        else
        Field[x1+1][y1] = 11+color-1;

        if(x1 % 2 == 1)
        {
            if(Field[x1-1][y1-1] < 3) Field[x1-1][y1-1] = 1+color-1;
            else
            if(Field[x1-1][y1-1] > 4 || Field[x1-1][y1-1] < 7) Field[x1-1][y1-1] = 5+color-1;
            else
            if(Field[x1-1][y1-1] > 6 || Field[x1-1][y1-1] < 10) Field[x1-1][y1-1] = 8+color-1;
            else
            Field[x1-1][y1-1] = 11+color-1;

            if(Field[x1+1][y1-1] < 3) Field[x1+1][y1-1] = 1+color-1;
            else
            if(Field[x1+1][y1-1] > 4 || Field[x1+1][y1-1] < 7) Field[x1+1][y1-1] = 5+color-1;
            else
            if(Field[x1+1][y1-1] > 6 || Field[x1+1][y1-1] < 10) Field[x1+1][y1-1] = 8+color-1;
            else
            Field[x1+1][y1-1] = 11+color-1;
        }
        else
        {
            if(Field[x1+1][y1+1] < 3) Field[x1+1][y1+1] = 1+color-1;
            else
            if(Field[x1+1][y1+1] > 4 || Field[x1+1][y1+1] < 7) Field[x1+1][y1+1] = 5+color-1;
            else
            if(Field[x1+1][y1+1] > 6 || Field[x1+1][y1+1] < 10) Field[x1+1][y1+1] = 8+color-1;
            else
            Field[x1+1][y1+1] = 11+color-1;

            if(Field[x1-1][y1+1] < 3) Field[x1-1][y1+1] = 1+color-1;
            else
            if(Field[x1-1][y1+1] > 4 || Field[x1-1][y1+1] < 7) Field[x1-1][y1+1] = 5+color-1;
            else
            if(Field[x1-1][y1+1] > 6 || Field[x1-1][y1+1] < 10) Field[x1-1][y1+1] = 8+color-1;
            else
            Field[x1-1][y1+1] = 11+color-1;
        }
}

void Paint2(int color, int x1, int y1)
{
        rec[Rec+1] = x1;
        rec[Rec+2] = y1;
        rec[Rec+3] = Field[x1][y1];

        rec[Rec+4] = x1;
        rec[Rec+5] = y1-1;
        rec[Rec+6] = Field[x1][y1-1];

        rec[Rec+7] = x1-1;
        rec[Rec+8] = y1;
        rec[Rec+9] = Field[x1-1][y1];

        rec[Rec+10] = x1+1;
        rec[Rec+11] = y1;
        rec[Rec+12] = Field[x1+1][y1];

        rec[Rec+13] = x1;
        rec[Rec+14] = y1+1;
        rec[Rec+15] = Field[x1][y1+1];

        Field[x1][y1] = 5+color-1;

        if(Field[x1][y1-1] < 3) Field[x1][y1-1] = 1+color-1;
        else
        if(Field[x1][y1-1] > 4 || Field[x1][y1-1] < 7) Field[x1][y1-1] = 5+color-1;
        else
        if(Field[x1][y1-1] > 6 || Field[x1][y1-1] < 10) Field[x1][y1-1] = 8+color-1;
        else
        Field[x1][y1-1] = 11+color-1;

        if(Field[x1-1][y1] < 3) Field[x1-1][y1] = 1+color-1;
        else
        if(Field[x1-1][y1] > 4 || Field[x1-1][y1] < 7) Field[x1-1][y1] = 5+color-1;
        else
        if(Field[x1-1][y1] > 6 || Field[x1-1][y1] < 10) Field[x1-1][y1] = 8+color-1;
        else
        Field[x1-1][y1] = 11+color-1;

        if(Field[x1][y1+1] < 3) Field[x1][y1+1] = 1+color-1;
        else
        if(Field[x1][y1+1] > 4 || Field[x1][y1+1] < 7) Field[x1][y1+1] = 5+color-1;
        else
        if(Field[x1][y1+1] > 6 || Field[x1][y1+1] < 10) Field[x1][y1+1] = 8+color-1;
        else
        Field[x1][y1+1] = 11+color-1;

        if(Field[x1+1][y1] < 3) Field[x1+1][y1] = 1+color-1;
        else
        if(Field[x1+1][y1] > 4 || Field[x1+1][y1] < 7) Field[x1+1][y1] = 5+color-1;
        else
        if(Field[x1+1][y1] > 6 || Field[x1+1][y1] < 10) Field[x1+1][y1] = 8+color-1;
        else
        Field[x1+1][y1] = 11+color-1;

        if(x1 % 2 == 1)
        {
            rec[Rec+16] = x1-1;
            rec[Rec+17] = y1-1;
            rec[Rec+18] = Field[x1-1][y1-1];

            rec[Rec+19] = x1+1;
            rec[Rec+20] = y1-1;
            rec[Rec+21] = Field[x1+1][y1-1];

            if(Field[x1-1][y1-1] < 3) Field[x1-1][y1-1] = 1+color-1;
            else
            if(Field[x1-1][y1-1] > 4 || Field[x1-1][y1-1] < 7) Field[x1-1][y1-1] = 5+color-1;
            else
            if(Field[x1-1][y1-1] > 6 || Field[x1-1][y1-1] < 10) Field[x1-1][y1-1] = 8+color-1;
            else
            Field[x1-1][y1-1] = 11+color-1;

            if(Field[x1+1][y1-1] < 3) Field[x1+1][y1-1] = 1+color-1;
            else
            if(Field[x1+1][y1-1] > 4 || Field[x1+1][y1-1] < 7) Field[x1+1][y1-1] = 5+color-1;
            else
            if(Field[x1+1][y1-1] > 6 || Field[x1+1][y1-1] < 10) Field[x1+1][y1-1] = 8+color-1;
            else
            Field[x1+1][y1-1] = 11+color-1;
        }
        else
        {
            rec[Rec+16] = x1+1;
            rec[Rec+17] = y1+1;
            rec[Rec+18] = Field[x1+1][y1+1];

            rec[Rec+19] = x1-1;
            rec[Rec+20] = y1+1;
            rec[Rec+21] = Field[x1-1][y1+1];

            if(Field[x1+1][y1+1] < 3) Field[x1+1][y1+1] = 1+color-1;
            else
            if(Field[x1+1][y1+1] > 4 || Field[x1+1][y1+1] < 7) Field[x1+1][y1+1] = 5+color-1;
            else
            if(Field[x1+1][y1+1] > 6 || Field[x1+1][y1+1] < 10) Field[x1+1][y1+1] = 8+color-1;
            else
            Field[x1+1][y1+1] = 11+color-1;

            if(Field[x1-1][y1+1] < 3) Field[x1-1][y1+1] = 1+color-1;
            else
            if(Field[x1-1][y1+1] > 4 || Field[x1-1][y1+1] < 7) Field[x1-1][y1+1] = 5+color-1;
            else
            if(Field[x1-1][y1+1] > 6 || Field[x1-1][y1+1] < 10) Field[x1-1][y1+1] = 8+color-1;
            else
            Field[x1-1][y1+1] = 11+color-1;
        }
}

bool Check3(int x, int y)
{
    if(Field[x][y] < 3) return true;
    return false;
}

bool Check2(int x, int y)
{
    if(x > M || x < 1 || y > N || y < 1) return false;
    return true;
}

bool Check(int Color, int x1, int y1)
{
    if(Field[x1][y1-1] == 1+Color-1 || Field[x1][y1-1] == 3+Color-1 || Field[x1][y1-1] == 5+Color-1 || Field[x1][y1-1] == 8+Color-1 || Field[x1][y1-1] == 11+Color-1) if(Check2(x1,y1-1) == true) return true;
    if(Field[x1-1][y1] == 1+Color-1 || Field[x1-1][y1] == 3+Color-1 || Field[x1-1][y1] == 5+Color-1 || Field[x1-1][y1] == 8+Color-1 || Field[x1-1][y1] == 11+Color-1) if(Check2(x1-1,y1) == true) return true;
    if(Field[x1][y1+1] == 1+Color-1 || Field[x1][y1+1] == 3+Color-1 || Field[x1][y1+1] == 5+Color-1 || Field[x1][y1+1] == 8+Color-1 || Field[x1][y1+1] == 11+Color-1) if(Check2(x1,y1+1) == true) return true;
    if(Field[x1+1][y1] == 1+Color-1 || Field[x1+1][y1] == 3+Color-1 || Field[x1+1][y1] == 5+Color-1 || Field[x1+1][y1] == 8+Color-1 || Field[x1+1][y1] == 11+Color-1) if(Check2(x1+1,y1) == true) return true;
    if(x1 % 2 == 1)
    {
        if(Field[x1-1][y1-1] == 1+Color-1 || Field[x1-1][y1-1] == 3+Color-1 || Field[x1-1][y1-1] == 5+Color-1 || Field[x1-1][y1-1] == 8+Color-1 || Field[x1-1][y1-1] == 11+Color-1) if(Check2(x1-1,y1-1) == true) return true;
        if(Field[x1+1][y1-1] == 1+Color-1 || Field[x1+1][y1-1] == 3+Color-1 || Field[x1+1][y1-1] == 5+Color-1 || Field[x1+1][y1-1] == 8+Color-1 || Field[x1+1][y1-1] == 11+Color-1) if(Check2(x1+1,y1-1) == true) return true;
    }
    else
    {
        Field[x1][y1];
        if(Field[x1+1][y1+1] == 1+Color-1 || Field[x1+1][y1+1] == 3+Color-1 || Field[x1+1][y1+1] == 5+Color-1 || Field[x1+1][y1+1] == 8+Color-1 || Field[x1+1][y1+1] == 11+Color-1) if(Check2(x1+1,y1+1) == true) return true;
        if(Field[x1-1][y1+1] == 1+Color-1 || Field[x1-1][y1+1] == 3+Color-1 || Field[x1-1][y1+1] == 5+Color-1 || Field[x1-1][y1+1] == 8+Color-1 || Field[x1-1][y1+1] == 11+Color-1) if(Check2(x1-1,y1+1) == true) return true;
    }
    return false;
}

bool Read()
{
    cin >> M >> N >> Turns >> Mode;
    cin >> m1 >> n1 >> m2 >> n2;
    if(m1 == m2 && n1 == n2) return false;
    if(m1 < 1 || m2 < 1 || m1 > M || m2 > M || n1 < 1 || n2 < 1 || n1 > N || n2 > N) return false;
    //if(Turns > (M + N) / 2) return false;
    if(Mode > 3 || Mode < 1) return false;
    Field[m1][n1] = 3;
    Field[m2][n2] = 4;
    if(Mode > 1)
    {
        cin >> Mount;
        for(int i = 1; i <= Mount; i++)
        {
            cin >> mount[i][1] >> mount[i][2];
            if(mount[i][1] == m1 && mount[i][2] == n1) return false;
            if(mount[i][1] == m2 && mount[i][2] == n2) return false;
            if(Field[mount[i][1]][mount[i][2]] != 0) return false;
            if(mount[i][1] < 1 || mount[i][1] > M || mount[i][2] < 1 || mount[i][2] > N) return false;
            Field[mount[i][1]][mount[i][2]] = 10;
        }
        cin >> Water;
        for(int i = 1; i <= Water; i++)
        {
            cin >> water[i][1] >> water[i][2];
            if(water[i][1] == m1 && water[i][2] == n1) return false;
            if(water[i][1] == m2 && water[i][2] == n2) return false;
            if(Field[water[i][1]][water[i][2]] != 0) return false;
            if(water[i][1] < 1 || water[i][1] > M || water[i][2] < 1 || water[i][2] > N) return false;
            Field[water[i][1]][water[i][2]] = 7;
        }
    }
        Paint(1, m1, n1);
        Paint(2, m2, n2);
        Field[m1][n1] = 3;
        Field[m2][n2] = 4;
        if(Mode < 3)
        {
            for(int i = 1; i <= Turns; i++)
            {
                cin >> turns[i][1] >> turns[i][2];
                if(turns[i][1] == m1 && turns[i][2] == n1) return false;
                if(turns[i][1] == m2 && turns[i][2] == n2) return false;
                if(Field[turns[i][1]][turns[i][2]] >= 3) return false;
                if(turns[i][1] < 1 || turns[i][1] > M || turns[i][2] < 1 || turns[i][2] > N) return false;
                if(!Check((i+1)%2+1, turns[i][1], turns[i][2])) return false;
                Paint((i+1)%2+1, turns[i][1], turns[i][2]);
            }
        }

    return true;
}

void Count()
{
    int kol1 = 1, kol2 = 1;
    for(int i = 1; i <= M; i++)
        for(int u = 1; u <= N; u++)
    {
        if(Field[i][u] == 1 || Field[i][u] == 5 || Field[i][u] == 8 || Field[i][u] == 11) kol1++;
        else
        if(Field[i][u] == 2 || Field[i][u] == 6 || Field[i][u] == 9 || Field[i][u] == 12) kol2++;
    }
    if(kol1 > kol2)
    cout << "First Player Win\n";
    else
    if(kol1 < kol2)
    cout << "Second Player Win\n";
    else
    cout << "Draw\n";
    cout << kol1 << " " << kol2;
}

void Count2(long long Color, long long x1, long long y1, long long numb)
{
        cost[numb].zn=0;
        if(Field[x1][y1] == 0) cost[numb].zn++;
        else
        if(Field[x1][y1] == 3-Color || Field[x1][y1] == 7 || Field[x1][y1] == 10) cost[numb].zn+=2;
        else
        if(Field[x1][y1] == 10-Color || Field[x1][y1] == 13-Color || Field[x1][y1] == 7-Color) cost[numb].zn+=3;

        if(Field[x1][y1-1] == 0) cost[numb].zn++;
        else
        if(Field[x1][y1-1] == 3-Color || Field[x1][y1-1] == 7 || Field[x1][y1-1] == 10) cost[numb].zn+=2;
        else
        if(Field[x1][y1-1] == 10-Color || Field[x1][y1-1] == 13-Color || Field[x1][y1-1] == 7-Color) cost[numb].zn+=3;

        if(Field[x1-1][y1] == 0) cost[numb].zn++;
        else
        if(Field[x1-1][y1] == 3-Color || Field[x1-1][y1] == 7 || Field[x1-1][y1] == 10) cost[numb].zn+=2;
        else
        if(Field[x1-1][y1] == 10-Color || Field[x1-1][y1] == 13-Color || Field[x1-1][y1] == 7-Color) cost[numb].zn+=3;

        if(Field[x1][y1+1] == 0) cost[numb].zn++;
        else
        if(Field[x1][y1+1] == 3-Color || Field[x1][y1+1] == 7 || Field[x1][y1+1] == 10) cost[numb].zn+=2;
        else
        if(Field[x1][y1+1] == 10-Color || Field[x1][y1+1] == 13-Color || Field[x1][y1+1] == 7-Color) cost[numb].zn+=3;

        if(Field[x1+1][y1] == 0) cost[numb].zn++;
        else
        if(Field[x1+1][y1] == 3-Color || Field[x1+1][y1] == 7 || Field[x1+1][y1] == 10) cost[numb].zn+=2;
        else
        if(Field[x1+1][y1] == 10-Color || Field[x1+1][y1] == 13-Color || Field[x1+1][y1] == 7-Color) cost[numb].zn+=3;

        if(x1 % 2 == 1)
        {
            if(Field[x1-1][y1-1] == 0) cost[numb].zn++;
            else
            if(Field[x1-1][y1-1] == 3-Color || Field[x1-1][y1-1] == 7 || Field[x1-1][y1-1] == 10) cost[numb].zn+=2;
            else
            if(Field[x1-1][y1-1] == 10-Color || Field[x1-1][y1-1] == 13-Color || Field[x1-1][y1-1] == 7-Color) cost[numb].zn+=3;

            if(Field[x1+1][y1-1] == 0) cost[numb].zn++;
            else
            if(Field[x1+1][y1-1] == 3-Color || Field[x1+1][y1-1] == 7 || Field[x1+1][y1-1] == 10) cost[numb].zn+=2;
            else
            if(Field[x1+1][y1-1] == 10-Color || Field[x1+1][y1-1] == 13-Color || Field[x1+1][y1-1] == 7-Color) cost[numb].zn+=3;
        }
        else
        {
            if(Field[x1+1][y1+1] == 0) cost[numb].zn++;
            else
            if(Field[x1+1][y1+1] == 3-Color || Field[x1+1][y1+1] == 7 || Field[x1+1][y1+1] == 10) cost[numb].zn+=2;
            else
            if(Field[x1+1][y1+1] == 10-Color || Field[x1+1][y1+1] == 13-Color || Field[x1+1][y1+1] == 7-Color) cost[numb].zn+=3;

            if(Field[x1-1][y1+1] == 0) cost[numb].zn++;
            else
            if(Field[x1-1][y1+1] == 3-Color || Field[x1-1][y1+1] == 7 || Field[x1-1][y1+1] == 10) cost[numb].zn+=2;
            else
            if(Field[x1-1][y1+1] == 10-Color || Field[x1-1][y1+1] == 13-Color || Field[x1-1][y1+1] == 7-Color) cost[numb].zn+=3;
        }
}

void AITUT1()
{
    int red = 0;
    for(int i = 1; i <= M; i++)
    for(int u = 1; u <= N; u++)
    {
        if(red == 1) break;
        else
        if(Check3(i, u) == true && Check((Turns%2)+1, i, u) == true)
        {
            Paint((Turns%2)+1, i, u);
            cout << i << " " << u;
            red = 1;
        }
    }
}

void AITUT2()
{
    int red = 1, red2 = 0;
    for(int i = 1; i <= M; i++)
    for(int u = 1; u <= N; u++)
    {
        if(Check3(i, u) == true && Check((Turns%2)+1, i, u) == true)
        {
            red2++;
            Count2((Turns%2)+1, i, u, red2);
            cost[red2].fi = i;
            cost[red2].se = u;
            if(cost[red2].zn >= cost[red].zn) red = red2;
        }
    }
            Paint((Turns%2)+1, cost[red].fi, cost[red].se);
            cout << cost[red].fi << " " << cost[red].se;
}
//universal
void AITUT3(long long High, long long Now, long long Numb)
{
    int red = Numb+1, red2 = Numb;
    if(High == Now)
    {
        //cout << 1;
        for(int i = 1; i <= M; i++)
        for(int u = 1; u <= N; u++)
        {
            if(Check3(i, u) == true && Check((Turns+Now)%2+1, i, u) == true)
            {
                red2++;
                Count2((Turns+Now)%2+1, i, u, red2);
                cost[red2].fi = i;
                cost[red2].se = u;
                if(cost[red2].zn >= cost[red].zn) red = red2;
            }
        }
        if(Now == 0)
        {cost[0].fi=cost[red].fi;
        cost[0].se=cost[red].se;}

        cost[Numb].zn+=cost[red].zn;
    }
    else
    {
        for(int i = 1; i <= M; i++)
        for(int u = 1; u <= N; u++)
        {
            if(Check3(i, u) == true && Check((Turns+Now)%2+1, i, u) == true)
            {
                //cout << Rec << "\n";
                red2++;
                Count2((Turns+Now)%2+1, i, u, red2);
                cost[red2].fi = i;
                cost[red2].se = u;

                Rec++;
                rec[Rec] = 1000021;
                Paint2((Turns+Now)%2+1, i, u);
                Rec+=21;

                AITUT3(High, Now+1, red2);
                if(cost[red2].zn >= cost[red].zn) red = red2;
                if(Rec >= 22)
                {
                    for(int t = Rec-20; t < Rec; t+=3)
                    {
                        Field[rec[t]][rec[t+1]] = rec[t+2];
                    }
                    Rec-=22;
                }
            }
        }
        if(Now == 0)
        {
        //red = 1;
        //for(int i = 1; i <= red2; i++) if(cost[i].zn > cost[red].zn) red = i;
        cost[0].fi=cost[red].fi;
        cost[0].se=cost[red].se;
        }

        cost[Numb].zn+=cost[red].zn;

    }

        if(Now == 0) {  Paint((Turns%2)+1, cost[0].fi, cost[0].se);
        cout << cost[0].fi << " " << cost[0].se;}
}

void Write(long long numb, long long Max)
{
    cout << M << " " << N << " " << Turns+1 << " " << Mode << "\n";
    cout << m1  << " " << n1 << " " << m2 << " " << n2 << "\n";
    if(Mode > 1)
    {
        cout << Mount << " ";
        for(int i = 1; i <= Mount; i++)
        {
            cout << mount[i][1] << " " << mount[i][2] << " ";
        }
        cout << "\n";
        cout << Water << " ";
        for(int i = 1; i <= Water; i++)
        {
            cout << water[i][1] << " " << water[i][2] << " ";
        }
        cout << "\n";
    }
    if(Mode < 3)
    {
        for(int i = 1; i <= Turns; i++)
        {
            cout << turns[i][1] << " " << turns[i][2] << "\n";
        }
        if(numb == 1)AITUT1();
        else
        if(numb == 2)AITUT2();
        else
        if(numb == 3)AITUT3(Max,0,0);
        cout << "\n";
    }
}

int main()
{
    freopen("input.txt","r",stdin);
    freopen("output.txt","w",stdout);
    Rec = 0;
    if (!Read()) cout << "ERROR";
    else
    {
    Write(3,4);
    Count();
    }
    return 0;
}
