using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeUML
{
    [System.Serializable]
    public class UseCaseRelation
    {
        [SerializeField]
        private List<Relation> listOfRelation;
        private RelationStateUseCase relationState;

        public UseCaseRelation()
        {
            listOfRelation = new List<Relation>();
            relationState = new RelationStateUseCase();
        }

        public void Draw(List<Actor> actors, List<UseCase> useCases)
        {
            for (int j = 0; j < actors.Count; j++)
            {
                int useCasePos = 0;
                for (int k = 0; k < useCases.Count; k++)
                {
                    for (int i = 0; i < listOfRelation.Count; i++)
                    {
                        if (listOfRelation[i].IsRelation(actors[j].ID, useCases[k].ID))
                        {
                            DrawUtils.DrawNodeCurveWithButton(actors[j].transform, useCases[k].transform, useCasePos, 30, delegate
                                {
                                    listOfRelation.Remove(listOfRelation[i]);
                                });
                            useCasePos++;
                        }
                    }
                }
            }
        }

        private void MakeRelation(int idActors, int idUseCases)
        {
            listOfRelation.Add(new Relation(idActors, idUseCases));
        }

        public void OnMakeStartRelation(int idActors)
        {
            relationState.isMakingRelationState = true;
            relationState.selectedActor = idActors;
        }

        public void OnDeleteUseCase(int id)
        {
            listOfRelation.RemoveAll((Relation obj) => obj.useCaseID == id);
        }

        public void OnDeleteActor(int id)
        {
            listOfRelation.RemoveAll((Relation obj) => obj.actorID == id);
        }

        public void OnSelectUseCase(int idUseCase)
        {
            if (relationState.isMakingRelationState)
            {
                relationState.isMakingRelationState = false;
                relationState.selectedUseCase = idUseCase;
                MakeRelation(relationState.selectedActor, relationState.selectedUseCase);
            }
        }
    }

    [System.Serializable]
    public class Relation
    {
        public int actorID;
        public int useCaseID;

        public Relation(int actorID, int useCaseID)
        {
            this.actorID = actorID;
            this.useCaseID = useCaseID;
        }

        public bool IsRelation(int actorID, int useCaseID)
        {
            return this.actorID == actorID && this.useCaseID == useCaseID;
        }
    }

    internal class RelationStateUseCase
    {
        public int selectedActor;
        public int selectedUseCase;

        public bool isMakingRelationState = false;
    }

}
