using APIManager.Recommender.Models.Requests;
using System;
using System.Net.Http.Headers;

namespace RecommenderSystem
{
    public static class Recommender
    {
        public static RecommenderState RecommenderState = new RecommenderState();

        public static void GetRecommendation()
        {
            RecommenderState.RecommendationId++;

            var res = APIManager.Recommender.RecommenderManager.Recommend(new RecommendRequest() { FirstSequence = RecommenderState.FirstSequence, LastSequence = RecommenderState.LastSequence, RecommendationId = RecommenderState.RecommendationId, SessionId = RecommenderState.SessionId});

            if (res == null)
            {
                GetRecommendation();
            }
            else if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                GetRecommendation();
            }
            else
            {
                if(RecommenderState.RecommendedMusic == res.Content.RecommendedId)
                {
                    RecommenderState.RecommendedMusic = -1;
                    RecommenderState.RecommendedMusic = res.Content.RecommendedId;
                }
                else
                {
                    RecommenderState.RecommendedMusic = res.Content.RecommendedId;
                }

            }

        }

        public static void Reset()
        {
            RecommenderState.FirstSequence = 0;
            RecommenderState.LastSequence = 0;
            RecommenderState.RecommendationId = 0;
            RecommenderState.RecommendedMusic = -1;
        }


    }
}
