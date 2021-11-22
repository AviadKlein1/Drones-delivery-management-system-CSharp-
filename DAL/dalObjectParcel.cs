using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            public partial class dalObject : IDal
            {
                /// <summary>
                /// add it to array
                /// </summary>
                /// returns new parcel's id
                public void addParcel(Parcel myParcel)
                {
                    //insert parcel to array
                    //IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex] = myParcel;
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myParcel.id)
                            throw new ExcistingIdException(myParcel.id, $"parcel already exist: {myParcel.id}");
                    IDAL.DO.DalObject.DataSource.parcels.Add(myParcel);
                }
                public int ParcelRunId()
                {
                    return IDAL.DO.DalObject.DataSource.Config.ParcelRunId++;
                }

                public IDAL.DO.Parcel getParcel(int myId)
                {
                    bool isDouble = false;
                    Parcel temp = new Parcel();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                        }
                    }
                    if (isDouble == false)
                        return temp;
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }
                /// <summary>
                /// print all parcels
                /// </summary>
                public IEnumerable<Parcel> getParcels()
                {
                    List<IDAL.DO.Parcel> temp = new List<IDAL.DO.Parcel>();
                    temp = IDAL.DO.DalObject.DataSource.parcels;
                    return temp;
                }
                /// <summary>
                /// print all parcels which are not associated with a drone
                /// </summary>
                //public IEnumerable<Parcel> getNotAssociatedParcels()
                //{
                //    //find size of new array for parcels
                //    List<IDAL.DO.Parcel> temp = new List<IDAL.DO.Parcel>();
                //    temp = IDAL.DO.DalObject.DataSource.parcels;
                //    int size = IDAL.DO.DalObject.DataSource.parcels.Count;
                //    return temp;
                //}
                ///// <summary>
                ///// report pacel picked up
                ///// </summary>
                ///// <param name="myId"></param>
                //public void pickUp(int myId)
                //{
                //    bool parcelExist = false;
                //    //int j = 0;
                //    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                //    //    j++;
                //    //IDAL.DO.DalObject.DataSource.parcels[j].pickedUp = DateTime.Now;
                //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                //        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                //            parcelExist = true;
                //    if (parcelExist == false)
                //        throw new WrongIdException(myId, $"wrong parcel id: {myId}");
                //    else
                //    {
                //        IDAL.DO.Parcel temp = IDAL.DO.DalObject.DataSource.parcels[i];
                //        temp.pickedUp = DateTime.Now;
                //        IDAL.DO.DalObject.DataSource.parcels[i] = temp;
                //    }
                //}

                ///// <summary>
                ///// report parcel delivered
                ///// </summary>
                ///// <param name="myId"></param>
                //public void delivered(int myId)
                //{
                //    bool parcelExist = false;
                //    //int j = 0;
                //    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                //    //    j++;
                //    //IDAL.DO.DalObject.DataSource.parcels[j].delivered = DateTime.Now;
                //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                //        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                //            parcelExist = true;
                //    if (parcelExist == false)
                //        throw new WrongIdException(myId, $"wrong parcel id: {myId}");
                //    else
                //    {
                //        IDAL.DO.Parcel temp = IDAL.DO.DalObject.DataSource.parcels[i];
                //        temp.delivered = DateTime.Now;
                //        IDAL.DO.DalObject.DataSource.parcels[i] = temp;
                //    }
                //}
            }
        }
    }
}